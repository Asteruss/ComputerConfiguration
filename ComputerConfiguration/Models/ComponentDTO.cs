using ComputerConfiguration.Commands;
using ComputerConfiguration.Models.Enums;
using System.ComponentModel;

namespace ComputerConfiguration.Models;

public class ComponentDTO : NotifyPropertyChanged
{
    private ComponentStatus _componentStatus = ComponentStatus.NotSelected;
    public ComponentStatus ComponentStatus
    {
        get => _componentStatus;
        set
        {
            _componentStatus = value;
            OnPropertyChanged();
        }
    }
}
