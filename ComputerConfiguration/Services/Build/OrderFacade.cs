using ComputerConfiguration.Builder;
using ComputerConfiguration.DB;
using ComputerConfiguration.DTO;
using ComputerConfiguration.Models;
using ComputerConfiguration.Models.Authentication;
using ComputerConfiguration.Models.Build;
using ComputerConfiguration.Models.Components;
using ComputerConfiguration.Models.Enums;
using ComputerConfiguration.Extensions;
using ComputerConfiguration.Models.Orders.Bonus;
using ComputerConfiguration.Models.UI;
using ComputerConfiguration.Services.Authentication;
using ComputerConfiguration.Services.Build.Compability;
using Microsoft.EntityFrameworkCore;

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
        IComputerBuildSession session, IAuthService authService, OrderService orderService, ComputerConfigurationDBContext dbContext)
    {
        _pricingService = pricingService;
        _bonusService = bonusService;
        _session = session;
        _compatibilityService = compatibilityService;
        _authService = authService;
        _orderService = orderService;
        _dbContext = dbContext;
    }
    public List<ComponentBase> GetComponents()
    {
        var comp = _session.GetDto();
        return [comp.Cpu, comp.Gpu, comp.Psu, comp.Cooler, comp.Case, comp.Motherboard, .. comp.Rams.Select(br => br.RamHelper), .. comp.Storages.Select(br => br.StorageHelper)];
    }
    public List<ComponentBase> GetComponentsUnqiue()
    {
        var comp = _session.GetDto();
        return [comp.Cpu, comp.Gpu, comp.Psu, comp.Cooler, comp.Case, comp.Motherboard,
            .. comp.Rams.Select(br => br.RamHelper).DistinctBy(r => r.Id),
            .. comp.Storages.Select(br => br.StorageHelper).DistinctBy(r => r.Id)];
    }
    public List<ComponentBase> GetComponentsNotNull() => [.. GetComponents().Where(c => c != null)];
    public List<ComponentBase> GetComponentsUnqiueNotNull() => [.. GetComponentsUnqiue().Where(c => c != null)];
    public List<ComponentCartDTO> GetComponentsDTO() =>
        [.. GetComponentsNotNull().Select(c => new ComponentCartDTO(c.ComponentCategory.ToDescriptionString(), c))];
    

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

    public int GetBonuses(bool useBonuses)
    {
        if (!_authService.IsAuthenticated) return 0;
        return useBonuses ? _bonusService.GetMaxBonusToSpend(_authService.CurrentUser!, GetPrice()) :
                            _bonusService.GetMaxBonusToEarn(_authService.CurrentUser!.PrivilegeLevel!, GetPrice());

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
    public async Task<IResult> CreateOrderAsync(bool useBonuses, Address address)
    {
        if (!_authService.IsAuthenticated)
            return new Error("Order", "User", "Пользователь не вошел в аккаунт");
        if (address == null)
            return new Error("Order", "Address", "Адрес не выбран");

        int userId = _authService.CurrentUser!.Id;
        var price = GetFinalPrice(useBonuses);
        if (price < 0)
            return new Error("Order", "Price", "Ошибка цены");
        if (price == 0)
            return new Error("Order", "Build", "Ничего не выбрано");

        var build = _session.GetBuild();

        AttachIfExists(build.Cpu);
        AttachIfExists(build.Gpu);
        AttachIfExists(build.Motherboard);
        AttachIfExists(build.Case);
        AttachIfExists(build.Cooler);
        AttachIfExists(build.Psu);

        foreach (var br in build.BuildRams)
        {
            if (br.RamId == 0)
                throw new InvalidOperationException("RamId не может быть 0");
            var ram = await _dbContext.Rams.FindAsync(br.RamId) ?? throw new InvalidOperationException($"Ram с Id {br.RamId} не найден");
            _dbContext.Entry(ram).State = EntityState.Unchanged;
        }

        foreach (var bs in build.BuildStorages)
        {
            if (bs.StorageId == 0)
                throw new InvalidOperationException("StorageId не может быть 0");
            var storage = await _dbContext.Storages.FindAsync(bs.StorageId) ?? throw new InvalidOperationException($"Storage с Id {bs.StorageId} не найден");
            _dbContext.Entry(storage).State = EntityState.Unchanged;
        }

        foreach (var option in build.AdditionalServices ?? Enumerable.Empty<AdditionalServiceOption>())
        {
            if (option.Id == 0)
                throw new InvalidOperationException("Id дополнительной опции не может быть 0");
            var existingOption = await _dbContext.AdditionalServiceOptions.FindAsync(option.Id) ?? throw new InvalidOperationException($"Опция с Id {option.Id} не найдена");
            _dbContext.Entry(existingOption).State = EntityState.Unchanged;
            if (existingOption.AdditionalService != null && existingOption.AdditionalService.Id > 0)
                _dbContext.Entry(existingOption.AdditionalService).State = EntityState.Unchanged;
        }

        _dbContext.ComputerBuilds.Add(build);
        await _dbContext.SaveChangesAsync();

        var order = _orderService.CreateOrder(userId, build.Id, price, address.Id);
        _dbContext.Orders.Add(order);

        await _dbContext.SaveChangesAsync();

        return new OrderSuccess("Order", "Payment", "Заказ успешно передан к оплате", order.Id, order.Price);
    }

    private void AttachIfExists(ComponentBase component)
    {
        if (component != null && component.Id > 0)
            _dbContext.Attach(component);
    }
    public async Task<IResult> CompleteOrderAsync(int orderId, bool useBonuses)
    {
        var user = _authService.CurrentUser!;
        int userid = user.Id;

        var order = await _orderService.GetOrderAsync(orderId, OrderStatus.WaitForPayment);
        if (order == null)
            return new Error("Order", "Order", "Заказ не найден");

        order.OrderStatus = OrderStatus.Accepted;
        await RemoveComponentsAsync(order.Id);
        BonusHistory bonusOP;
        bonusOP = (useBonuses) ? _bonusService.SpendBonuses(user, order) :
                                 _bonusService.EarnBonuses(user, order);

        await _dbContext.BonusHistory.AddAsync(bonusOP);
        //_bonusService.ChangePrivilegeLevel(user, _dbContext.PrivilegeLevels, _orderService.GetOrderCostsBy(userid));
        await _dbContext.SaveChangesAsync();
        _session.Reset();

        return new Success("Order", "Final", "Заказ успешно создан");

    }
    public async Task<IResult> RemoveComponentsAsync(int orderId)
    {
        var order = await _orderService.GetOrderAsync(orderId, OrderStatus.WaitForPayment);
        if (order == null)
            return new Error("Order", "Order", "Заказ не найден");

        var components = GetComponentsUnqiueNotNull();
        foreach (var component in components)
            await UpdateComponentCount(component);

        await _dbContext.SaveChangesAsync();

        return new Success("Order", "Build", "Все компоненты списаны");
    }

    private async Task UpdateComponentCount(ComponentBase component)
    {
        var id = component.Id;
        var count = component.Count;

        switch (component)
        {
            case Ram:
                await _dbContext.Rams.Where(r => r.Id == id)
                    .ExecuteUpdateAsync(s => s.SetProperty(r => r.Count, count));
                break;
            case Storage:
                await _dbContext.Storages.Where(r => r.Id == id)
                    .ExecuteUpdateAsync(s => s.SetProperty(r => r.Count, count));
                break;
            case Cpu:
                await _dbContext.Cpus.Where(r => r.Id == id)
                    .ExecuteUpdateAsync(s => s.SetProperty(r => r.Count, count));
                break;
            case Gpu:
                await _dbContext.Gpus.Where(r => r.Id == id)
                    .ExecuteUpdateAsync(s => s.SetProperty(r => r.Count, count));
                break;
            case Motherboard:
                await _dbContext.Motherboards.Where(r => r.Id == id)
                    .ExecuteUpdateAsync(s => s.SetProperty(r => r.Count, count));
                break;
            case Case:
                await _dbContext.Cases.Where(r => r.Id == id)
                    .ExecuteUpdateAsync(s => s.SetProperty(r => r.Count, count));
                break;
            case Cooler:
                await _dbContext.Coolers.Where(r => r.Id == id)
                    .ExecuteUpdateAsync(s => s.SetProperty(r => r.Count, count));
                break;
            case Psu:
                await _dbContext.Psus.Where(r => r.Id == id)
                    .ExecuteUpdateAsync(s => s.SetProperty(r => r.Count, count));
                break;
        }
    }


    public async Task<IResult> DeleteOrderAsync(int orderId)
    {
        var order = await _orderService.GetOrderAsync(orderId, OrderStatus.WaitForPayment);
        _dbContext.ComputerBuilds.Remove(order.ComputerBuild);
        _dbContext.Orders.Remove(order);
        _session.Reset();
        return new Success("Order", "Final", "Заказ удален");

    }
}
