using ComputerConfiguration.Commands;
using ComputerConfiguration.DTO;
using ComputerConfiguration.Extensions;
using ComputerConfiguration.Models.Authentication;
using ComputerConfiguration.Services.Authentication;
using ComputerConfiguration.Services.Build;
using ComputerConfiguration.Services.Navigation;
using System.Windows.Navigation;

namespace ComputerConfiguration.ViewModels;

public class AddressAddingViewModel : ViewModelBase
{
    public AddressDTO AddressDTO { get; set; }
    private int _addressId;
    public RelayCommand Command { get; init; }
    public string CommandText { get; init; }

    private RelayCommand _addAddress;
    public RelayCommand AddAddress
    {
        get => _addAddress ??= new(async (add) =>
        {
            if (add is AddressDTO address)
            {
                await _addressService.AddAddressAsync(address, _authService.CurrentUser!.Id);
                _navigationService.GoBackReload();
            }
        });
    }
    private RelayCommand _editAddress;
    public RelayCommand EditAddress
    {
        get => _editAddress ??= new(async (add) =>
        {
            if (add is AddressDTO address)
            {
                await _addressService.EditAddressAsync(address, _addressId, _authService.CurrentUser!.Id);
                _navigationService.GoBack();
            }
        });
    }
    private readonly INavigationService _navigationService;
    private readonly IAuthService _authService;
    private readonly IAddressService _addressService;
    public AddressAddingViewModel(
        Address address,
        IAddressService addressSerivce,
        IAuthService authService,
        INavigationService navigationService)
    {
        AddressDTO = address.ToDto();
        _addressId = address.Id;
        Command = EditAddress;
        CommandText = "Изменить адрес";
        _addressService = addressSerivce;
        _authService = authService;
        _navigationService = navigationService;
    }

    public AddressAddingViewModel(
        IAddressService addressSerivce,
        IAuthService authService,
        INavigationService navigationService)
    {
        AddressDTO = new();
        Command = AddAddress;
        CommandText = "Добавить адрес";
        _addressService = addressSerivce;
        _authService = authService;
        _navigationService = navigationService;

    }

}
