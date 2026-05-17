using ComputerConfiguration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerConfiguration.ViewModels;

public class SortOptionViewModel : ViewModelBase
{
    public string DisplayName { get; }
    private bool? _isAscending = null;
    public bool? IsAscending
    {
        get => _isAscending;
        set
        {
            if (_isAscending == null)
                _isAscending = true;
            else
                _isAscending = value;
            OnPropertyChanged();
        }
    }

    public Func<IEnumerable<ComponentBase>, IOrderedEnumerable<ComponentBase>> SortAscending { get; }
    public Func<IEnumerable<ComponentBase>, IOrderedEnumerable<ComponentBase>> SortDescending { get; }
    public Func<IEnumerable<ComponentBase>, IOrderedEnumerable<ComponentBase>> Sort() =>
        IsAscending ?? true ? SortAscending : SortDescending;
    public SortOptionViewModel(
        string displayName,
        Func<IEnumerable<ComponentBase>, IOrderedEnumerable<ComponentBase>> sortAscending,
        Func<IEnumerable<ComponentBase>, IOrderedEnumerable<ComponentBase>> sortDescending = null)
    {
        DisplayName = displayName;
        SortAscending = sortAscending;
        SortDescending = sortDescending;
    }
}
