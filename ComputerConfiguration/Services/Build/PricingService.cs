using ComputerConfiguration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerConfiguration.Services.Build;

public class PricingService
{
    public double GetPriceForComponents(IEnumerable<IComponent> components) => components.Sum(c => c.BasePrice);
}
