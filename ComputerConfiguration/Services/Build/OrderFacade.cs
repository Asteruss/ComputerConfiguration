using ComputerConfiguration.Builder;
using ComputerConfiguration.Models;
using ComputerConfiguration.Models.Authentication;

namespace ComputerConfiguration.Services.Build;

public class OrderFacade
{
    private readonly IComputerBuildDtoBuilder _dtoBuilder;
    private readonly PricingService _pricingService;
    private readonly BonusService _bonusService;
    public OrderFacade(PricingService pricingService, BonusService bonusService, IComputerBuildDtoBuilder dtoBuilder)
    {
        _pricingService = pricingService;
        _bonusService = bonusService;
        _dtoBuilder = dtoBuilder;
    }
    private List<ComponentBase> GetComponents()
    {
        var comp = _dtoBuilder.BuildDto();
        return [comp.Cpu, comp.Gpu, comp.Psu, comp.Cooler, comp.Case, comp.Motherboard, ..comp.Rams, ..comp.Storages];
    }
    public double GetFinalPrice(bool useBonuses = false)
    {
        var comp = _dtoBuilder.BuildDto();
        double price = _pricingService.GetPriceForComponents(GetComponents()) + _pricingService.GetPriceForAdditiontalOptions(comp.SelectedAdditionalServices!);
        //if (useBonuses)
        //    price = _bonusService.GetMaxBonusToSpend(user, price);
        return price;
    }
}
