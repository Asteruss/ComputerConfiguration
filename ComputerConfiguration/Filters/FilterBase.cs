using ComputerConfiguration.Commands;
using ComputerConfiguration.Filters.Strategies;
using ComputerConfiguration.Models;

namespace ComputerConfiguration.Filters;

public abstract class FilterBase : NotifyPropertyChanged
{
    private object _value;
    public string Name { get; set; }         
    public string DisplayName { get; set; }
    public IMatchStrategy MatchStrategy { get; set; }
    public object Value
    {
        get => _value;
        set { _value = value; OnPropertyChanged(); }
    }
    public abstract bool Matches(IComponent component);
}
