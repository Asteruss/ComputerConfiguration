using ComputerConfiguration.Commands;
using ComputerConfiguration.DTO;
using ComputerConfiguration.Services.Authentication;
using ComputerConfiguration.Services.Navigation;
using System;
namespace ComputerConfiguration.ViewModels;

class RegistrationViewModel : ViewModelBase
{
    public UserRegistrationDTO RegistrationDTO { get; set; } = new();
    private RelayCommand _registerCommand;
    private readonly IAuthService _authService;
    private readonly INavigationService _navigationService;
    public RelayCommand RegisterCommand
    {
        get => _registerCommand ??= new RelayCommand(async (obj) =>
        {
            if (obj is UserRegistrationDTO user)
            {
                await _authService.RegisterAsync(user);
                await _authService.LoginAsync(new UserEntryDTO() { Email = user.Email, Password = user.Password});
                _navigationService.GoBack();
            }
        });
    }
    public RegistrationViewModel(IAuthService authService, INavigationService navigationService)
    {
        _authService = authService;
        _navigationService = navigationService;
    }
}
