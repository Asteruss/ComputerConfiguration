using ComputerConfiguration.Commands;
using ComputerConfiguration.Converters;
using ComputerConfiguration.Models.Authentication;
using ComputerConfiguration.Services.Authentication;
using ComputerConfiguration.Services.Build;
using ComputerConfiguration.Services.Navigation;
using System.Collections.ObjectModel;

namespace ComputerConfiguration.ViewModels;

public class AddressListViewModel : ViewModelBase
{
    private readonly IAddressService _addressServer;
    private readonly INavigationService _navigationService;
    private readonly IAuthService _authService;
    public ObservableCollection<Address> Addresses { get; set; }
    private RelayCommand _addCommand;
    public RelayCommand AddCommand
    {
        get => _addCommand ??= new(async (obj) =>_navigationService.NavigateTo<AddressAddingViewModel>());
    }
    private RelayCommand _editCommand;
    public RelayCommand EditCommand
    {
        get => _editCommand ??= new((add) =>
        {
            if (add is Address address)      
                _navigationService.NavigateTo<AddressAddingViewModel>(address);
            
        });
    }
    private RelayCommand _deleteCommand;
    public RelayCommand DeleteCommand
    {
        get => _deleteCommand ??= new(async (add) =>
        {
            if (add is Address address)
            {
                await _addressServer.DeleteAddressAsync(address);
                await LoadAddressesAsync();

            }
        });
    }

    private async Task LoadAddressesAsync()
    {
        Addresses = (await _addressServer.GetAddressAsync(_authService.CurrentUser!.Id)).ToObservableCollection();
        OnPropertyChanged(nameof(Addresses));
    }


    public AddressListViewModel(
        IAddressService addressServer,
        INavigationService navigationService,
        IAuthService authService)
    {
        _addressServer = addressServer;
        _navigationService = navigationService;
        _authService = authService;
        _ = LoadAddressesAsync();
    }
}
