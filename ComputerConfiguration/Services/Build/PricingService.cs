using ComputerConfiguration.Models;
using ComputerConfiguration.Models.Build;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerConfiguration.Services.Build;

public class PricingService
{
    public double GetPriceForComponents(IEnumerable<IComponent> components) => components.Where(c => c != null).Sum(c => c.BasePrice);
    public double GetPriceForAdditiontalOptions(IEnumerable<AdditionalServiceOption> options) => options.Sum(a => a.AdditionalPrice);
}
