using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerConfiguration.Models;

namespace ComputerConfiguration.Filters;

public class RangeFilter : FilterBase
{
    private double _min;
    private double _max;
    private double _currentMin;
    private double _currentMax;

    public double Min { get => _min; set { _min = value; CurrentMin = _min; OnPropertyChanged(); } }
    public double Max { get => _max; set { _max = value; CurrentMax = _max; OnPropertyChanged(); } }
    public double CurrentMin
    {
        get => _currentMin;
        set { _currentMin = value; OnPropertyChanged(); OnPropertyChanged(nameof(Value)); }
    }
    public double CurrentMax
    {
        get => _currentMax;
        set { _currentMax = value; OnPropertyChanged(); OnPropertyChanged(nameof(Value)); }
    }

    public override bool Matches(IComponent component)
    {
        var propValue = component.GetType().GetProperty(Name)?.GetValue(component);
        var range = (CurrentMin, CurrentMax);
        return MatchStrategy.IsMatch(propValue, range);
    }
}
