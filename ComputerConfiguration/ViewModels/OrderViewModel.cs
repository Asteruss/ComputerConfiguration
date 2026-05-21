using ComputerConfiguration.Commands;
using ComputerConfiguration.Converters;
using ComputerConfiguration.Models.Authentication;
using ComputerConfiguration.Models.Orders;
using ComputerConfiguration.Models.UI;
using ComputerConfiguration.Services.Authentication;
using ComputerConfiguration.Services.Build;
using ComputerConfiguration.Services.Navigation;
using ComputerConfiguration.Services.Payment;
using System.Collections.ObjectModel;

namespace ComputerConfiguration.ViewModels;

public class OrderViewModel : ViewModelBase
{
    private readonly OrderFacade _orderFacade;
    private readonly IPaymentService _paymentService;
    private readonly INavigationService _navigationService;
    public bool UseBonuses { get; set; }
    public double FinalPrice { get; init; }
    public int Bonuses { get; init; }
    public IAuthService AuthService { get; init; }
    public ObservableCollection<Address> Addresses { get; set; }
    private Address _selectedAddress;
    public Address SelectedAddress
    {
        get => _selectedAddress;
        set { _selectedAddress = value; OnPropertyChanged(); }
    }
    public ObservableCollection<PaymentMethod> PaymentMethods { get; set; }
    private PaymentMethod _selectedPaymentMethod;
    public PaymentMethod SelectedPaymentMethod
    {
        get => _selectedPaymentMethod;
        set { _selectedPaymentMethod = value; OnPropertyChanged(); }
    }


    private RelayCommand _buyCommand;
    public RelayCommand BuyCommand
    {
        get => _buyCommand ??= new(async _ =>
        {
            var result = await _orderFacade.CreateOrderAsync(UseBonuses, SelectedAddress);
            if (result.IsError())
                return;
            var order = result.AsOrderSuccess();
            bool res = true;
            if (SelectedPaymentMethod.IsOnline)
                res = await _paymentService.ProcessPaymentAsync(order.OrderId, SelectedPaymentMethod.Id, order.OrderPrice);
            if (!res)
                await _orderFacade.DeleteOrderAsync(order.OrderId);
            else
                await _orderFacade.CompleteOrderAsync(order.OrderId, UseBonuses);
            _navigationService.NavigateTo<HomeViewModel>();
        },
            _ => SelectedAddress != null && SelectedPaymentMethod != null && AuthService.IsAuthenticated
        );
    }
    private RelayCommand _backCommand;
    public RelayCommand BackCommand
    {
        get => _backCommand ??= new(_ => _navigationService.GoBack());
    }
    public OrderViewModel(
        OrderFacade orderFacade,
        INavigationService navigationService,
        IAuthService authService,
        IPaymentService paymentService,
        IAddressService addressService,
        bool useBonuses)
    {
        _orderFacade = orderFacade;
        _navigationService = navigationService;
        _paymentService = paymentService;
        AuthService = authService;
        UseBonuses = useBonuses;
        Addresses = addressService.GetAddress(AuthService.CurrentUser.Id).ToObservableCollection();
        PaymentMethods = _paymentService.GetPaymentMethods().ToObservableCollection();
        FinalPrice = _orderFacade.GetFinalPrice(useBonuses);
        Bonuses = _orderFacade.GetBonuses(useBonuses);

    }
}
