using ComputerConfiguration.Models;
using ComputerConfiguration.Models.Build;
using ComputerConfiguration.Models.Enums;

namespace ComputerConfiguration.Services.Build;

public class PricingService
{
    public double GetPriceForComponents(IEnumerable<ComponentBase> components) => components
        .Where(c => c != null && (c.ComponentStatus == ComponentStatus.Selected || c.ComponentStatus == ComponentStatus.SelectedMany))
        .Sum(c => c.BasePrice);
    public double GetPriceForAdditiontalOptions(IEnumerable<AdditionalServiceOption> options) => options.Sum(a => a.AdditionalPrice);
}
