using ComputerConfiguration.Commands;
using ComputerConfiguration.ViewModels;

namespace ComputerConfiguration.Services.Navigation;

public class NavigationStore : NotifyPropertyChanged, INavigationTarget
{
    private ViewModelBase _currentViewModel;
    public ViewModelBase CurrentViewModel
    {
        get => _currentViewModel;
        set
        {
            _currentViewModel = value;
            OnPropertyChanged();
        }
    }
}
