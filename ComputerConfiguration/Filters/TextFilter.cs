using ComputerConfiguration.Models;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ComputerConfiguration.Filters;

public class TextFilter : FilterBase
{
    public override bool Matches(IComponent component)
    {
        string text = Value as string;
        if (string.IsNullOrWhiteSpace(text)) return true;
        var propValue = component.GetType().GetProperty(Name)?.GetValue(component);
        return MatchStrategy.IsMatch(propValue, text);
    }
}
