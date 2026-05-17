using ComputerConfiguration.Commands;
using ComputerConfiguration.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerConfiguration.Filters;

public class SingleChoiceFilter : FilterBase
{
    private RelayCommand _resetCommand;
    public RelayCommand ResetCommand
    {
        get => _resetCommand ?? (_resetCommand = new((obj) => Value = null));
    }
    public ObservableCollection<object> Options { get; set; } = new();
    public override bool Matches(ComponentBase component)
    {
        if (Value == null) return true;
        var propValue = component.GetType().GetProperty(Name)?.GetValue(component);
        return MatchStrategy.IsMatch(propValue, Value);
    }
}
