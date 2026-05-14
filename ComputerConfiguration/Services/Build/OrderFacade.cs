using ComputerConfiguration.Builder;
using ComputerConfiguration.DTO;
using ComputerConfiguration.Models;
using ComputerConfiguration.Models.Enums;
using ComputerConfiguration.Models.Authentication;
using ComputerConfiguration.Services.Build.Compability;

namespace ComputerConfiguration.Services.Build;

public class OrderFacade
{
    private readonly IComputerBuildDtoBuilder _dtoBuilder;
    private readonly PricingService _pricingService;
    private readonly BonusService _bonusService;
    private readonly CompatibilityCheckerService _compatibilityService;
    public OrderFacade(PricingService pricingService, BonusService bonusService, CompatibilityCheckerService compatibilityService,
        IComputerBuildDtoBuilder dtoBuilder)
    {
        _pricingService = pricingService;
        _bonusService = bonusService;
        _dtoBuilder = dtoBuilder;
        _compatibilityService = compatibilityService;
    }
    public List<ComponentBase> GetComponents()
    {
        var comp = _dtoBuilder.BuildDto();
        return [comp.Cpu, comp.Gpu, comp.Psu, comp.Cooler, comp.Case, comp.Motherboard, ..comp.Rams, ..comp.Storages];
    }
    public List<ComponentBase> GetComponentsNotNull() => GetComponents().Where(c => c != null).ToList();
    public List<ComponentCartDTO> GetComponentsDTO()
    {
        var comp = _dtoBuilder.BuildDto();
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
        var comp = _dtoBuilder.BuildDto();
        double price = _pricingService.GetPriceForComponents(GetComponents()) + _pricingService.GetPriceForAdditiontalOptions(comp.SelectedAdditionalServices!);
        return price;
    }
    public double GetFinalPrice(User user, bool useBonuses = false)
    {
        var price = GetPrice();
        if (user != null && useBonuses)
            price = _bonusService.GetNewPrice(user, price);
        return price;
    }

    public double GetBonusEarn(User user)
    {
        if (user == null) return 0;
        return _bonusService.GetMaxBonusToEarn(user.PrivilegeLevel, GetPrice());
    }

    public double GetBonusSpend(User user)
    {
        if (user == null) return 0;
        return _bonusService.GetMaxBonusToSpend(user, GetPrice());
    }
    public (List<CompabilityErrorDTO>, bool) CheckCompability()
    {
        var checkRes = _compatibilityService.Check(_dtoBuilder.BuildDto());
        var res = new List<CompabilityErrorDTO>();
        foreach (var error in checkRes.Errors)
            res.Add(new(CompatibilityRuleEnum.Error, error));
        foreach (var warning in checkRes.Warnings)
            res.Add(new(CompatibilityRuleEnum.Warning, warning));
        foreach (var cmp in checkRes.NotFoundComponents.Distinct())
            res.Add(new(CompatibilityRuleEnum.ComponentNotFound, cmp));
        return (res, checkRes.IsCompatible);
    }
}
