using ComputerConfiguration.Commands;
using ComputerConfiguration.Models.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerConfiguration.Models;

public class ComponentDTO : NotifyPropertyChanged
{
    [NotMapped]
    private ComponentStatus _componentStatus = ComponentStatus.NotSelected;
    [NotMapped]
    public ComponentStatus ComponentStatus
    {
        get => _componentStatus;
        set
        {
            _componentStatus = value;
            OnPropertyChanged();
        }
    }

    // для ram и storage(подсчет каждого выбранного)
    [NotMapped]
    private int _countSelected = 0;
    [NotMapped]
    public int CountSelected
    {
        get => _countSelected;
        set
        {
            _countSelected = value;
            OnPropertyChanged();
        }
    }
    [NotMapped]
    private int _countFakeSelected = 0;
    [NotMapped]
    public int CountFakeSelected
    {
        get => _countFakeSelected;
        set
        {
            _countFakeSelected = value;
            OnPropertyChanged();
        }
    }
}
