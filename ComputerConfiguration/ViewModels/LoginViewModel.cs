using ComputerConfiguration.Commands;
using ComputerConfiguration.DTO;
using ComputerConfiguration.Services.Authentication;
using ComputerConfiguration.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerConfiguration.ViewModels;

public class LoginViewModel : ViewModelBase
{
    public UserEntryDTO EntryDTO { get; set; } = new();
    private RelayCommand _loginCommand;
    private readonly IAuthService _authService;
    private readonly INavigationService _navigationService;
    public RelayCommand LoginCommand
    {
        get => _loginCommand ??= new RelayCommand(async (obj) =>
        {
            if (obj is UserEntryDTO user)
            {
                await _authService.LoginAsync(user);
                _navigationService.GoBack();
            }
        });
    }
    public LoginViewModel(IAuthService authService, INavigationService navigationService)
    {
        _authService = authService;
        _navigationService = navigationService;
    }
}
