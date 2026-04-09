using ComputerConfiguration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerConfiguration.Filters;

public class TextFilter : FilterBase
{
    public override bool Matches(IComponent component)
    {
        if (string.IsNullOrWhiteSpace(Value?.ToString())) return true;
        var prop = component.GetType().GetProperty(Name)?.GetValue(component)?.ToString();
        return prop?.IndexOf(Value.ToString(), StringComparison.OrdinalIgnoreCase) >= 0;
    }
}
