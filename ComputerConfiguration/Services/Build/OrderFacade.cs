using ComputerConfiguration.Builder;
using ComputerConfiguration.DTO;
using ComputerConfiguration.Models;
using ComputerConfiguration.Models.Enums;
using ComputerConfiguration.Models.Authentication;
using ComputerConfiguration.Services.Build.Compability;
using ComputerConfiguration.Services.Authentication;
using ComputerConfiguration.DB;
using ComputerConfiguration.Models.Orders;
using ComputerConfiguration.Models.UI;
using ComputerConfiguration.Models.Orders.Bonus;

namespace ComputerConfiguration.Services.Build;

public class OrderFacade
{
    private readonly IComputerBuildSession _session;
    private readonly PricingService _pricingService;
    private readonly BonusService _bonusService;
    private readonly CompatibilityCheckerService _compatibilityService;
    private readonly IAuthService _authService;
    private readonly ComputerConfigurationDBContext _dbContext;
    private readonly OrderService _orderService;
    public OrderFacade(PricingService pricingService, BonusService bonusService, CompatibilityCheckerService compatibilityService,
        IComputerBuildSession session, IAuthService authService)
    {
        _pricingService = pricingService;
        _bonusService = bonusService;
        _session = session;
        _compatibilityService = compatibilityService;
        _authService = authService;
    }
    public List<ComponentBase> GetComponents()
    {
        var comp = _session.GetDto();
        return [comp.Cpu, comp.Gpu, comp.Psu, comp.Cooler, comp.Case, comp.Motherboard, .. comp.Rams, .. comp.Storages];
    }
    public List<ComponentBase> GetComponentsNotNull() => GetComponents().Where(c => c != null).ToList();
    public List<ComponentCartDTO> GetComponentsDTO()
    {
        var comp = _session.GetDto();
        var res = new List<ComponentCartDTO>();

        if (comp.Cpu != null)
            res.Add(new ComponentCartDTO("Процессор", comp.Cpu));
        if (comp.Gpu != null)
            res.Add(new ComponentCartDTO("Видеокарта", comp.Gpu));
        if (comp.Motherboard != null)
            res.Add(new ComponentCartDTO("Материнская плата", comp.Motherboard));
        if (comp.Case != null)
            res.Add(new ComponentCartDTO("Корпус", comp.Case));
        if (comp.Cooler != null)
            res.Add(new ComponentCartDTO("Охлаждение", comp.Cooler));
        if (comp.Psu != null)
            res.Add(new ComponentCartDTO("Блок питания", comp.Psu));

        if (comp.Rams != null && comp.Rams.Any())
        {
            foreach (var ram in comp.Rams)
                res.Add(new ComponentCartDTO("Оперативная память", ram));
        }
        if (comp.Storages != null && comp.Storages.Any())
        {
            foreach (var storage in comp.Storages)
                res.Add(new ComponentCartDTO("Накопитель", storage));
        }

        return res;
    }

    private double GetPrice()
    {
        var comp = _session.GetDto();
        double price = _pricingService.GetPriceForComponents(GetComponents()) + _pricingService.GetPriceForAdditiontalOptions(comp.SelectedAdditionalServices!);
        return price;
    }
    public double GetFinalPrice(bool useBonuses = false)
    {
        var price = GetPrice();
        if (_authService.IsAuthenticated && useBonuses)
            price = _bonusService.GetNewPrice(_authService.CurrentUser!, price);
        return price;
    }
    public double GetBonusEarn()
    {
        if (!_authService.IsAuthenticated) return 0;
        return _bonusService.GetMaxBonusToEarn(_authService.CurrentUser!.PrivilegeLevel!, GetPrice());
    }

    public double GetBonusSpend()
    {
        if (!_authService.IsAuthenticated) return 0;
        return _bonusService.GetMaxBonusToSpend(_authService.CurrentUser!, GetPrice());
    }
    public (List<CompabilityErrorDTO>, bool) CheckCompability()
    {
        var checkRes = _compatibilityService.Check(_session.GetDto());
        var res = new List<CompabilityErrorDTO>();
        foreach (var error in checkRes.Errors)
            res.Add(new(CompatibilityRuleEnum.Error, error));
        foreach (var warning in checkRes.Warnings)
            res.Add(new(CompatibilityRuleEnum.Warning, warning));
        foreach (var cmp in checkRes.NotFoundComponents.Distinct())
            res.Add(new(CompatibilityRuleEnum.ComponentNotFound, cmp));
        return (res, checkRes.IsCompatible);
    }
    public async Task<IResult> Buy(bool useBonuses, Address address)
    {
        if (!_authService.IsAuthenticated)
            return new Error("Order", "User", "Пользовать не вошел в аккаунт");
        if (address == null)
            return new Error("Order", "Address", "Адрес не выбран");

        var user = _authService.CurrentUser!;
        int userid = user.Id;
        var price = GetFinalPrice(useBonuses);
        if (price < 0)
            return new Error("Order", "Price", "Ошибка цены");

        if (price == 0)
            return new Error("Order", "Build", "Ничего не выбрано");


        var build = _session.GetBuild();
        build = _dbContext.ComputerBuilds.Add(build).Entity;

        Order order = _orderService.CreateOrder(userid, build.Id, price, address.Id);
        BonusHistory bonusOP;
        bonusOP = (useBonuses) ? _bonusService.SpendBonuses(user, order) :
                                 _bonusService.EarnBonuses(user, order);


        await _dbContext.Orders.AddAsync(order);
        await _dbContext.BonusHistory.AddAsync(bonusOP);
        _bonusService.ChangePrivilegeLevel(user, _dbContext.PrivilegeLevels, _orderService.GetOrderCostsBy(userid));
        await _dbContext.SaveChangesAsync();
        return new Success("Order", "Final", "Заказ успешно создан");
    }
}
