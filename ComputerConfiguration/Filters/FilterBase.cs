using ComputerConfiguration.ViewModels;
using ComputerConfiguration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerConfiguration.Filters;

public abstract class FilterBase : ViewModelBase
{
    private object _value;
    public string Name { get; set; }         
    public string DisplayName { get; set; }   
    public object Value
    {
        get => _value;
        set { _value = value; OnPropertyChanged(); }
    }
    public abstract bool Matches(IComponent component);
}
