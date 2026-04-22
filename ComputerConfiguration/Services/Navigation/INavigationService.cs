using ComputerConfiguration.ViewModels;

namespace ComputerConfiguration.Services.Navigation;

public interface INavigationService
{
    void NavigateTo<TViewModel>(params object[] parameters) where TViewModel : ViewModelBase;
    void GoBack();
    bool CanGoBack { get; } 
}
