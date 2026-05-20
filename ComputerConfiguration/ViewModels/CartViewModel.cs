using ComputerConfiguration.Builder;
using ComputerConfiguration.Commands;
using ComputerConfiguration.Converters;
using ComputerConfiguration.DTO;
using ComputerConfiguration.Repositories.ServicesRepository;
using ComputerConfiguration.Services.Authentication;
using ComputerConfiguration.Services.Build;
using ComputerConfiguration.Services.Navigation;
using System.Collections.ObjectModel;

namespace ComputerConfiguration.ViewModels;

public class CartViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    public IComputerBuildSession ComputerSession;
    private readonly OrderFacade _orderFacade;
    public IAuthService AuthService { get; init; }
    public ObservableCollection<ServiceSelectionViewModel> ServiceItems { get;} = new();
    public ObservableCollection<ComponentCartDTO> Components { get; set; } = new();
    public ObservableCollection<CompabilityErrorDTO> Errors { get; set;  } = new();
    public bool IsCompatible { get; set; }
    public bool IsAnyErrors { get; set; }
    public bool IsOrderCreation => IsCompatible && AuthService.IsAuthenticated;

    private double _totalPrice;
    public double TotalPrice
    {
        get => _totalPrice;
        set
        {
            _totalPrice = value;
            OnPropertyChanged();
        }
    }
    private double _bonuses;
    public double Bonuses
    {
        get => _bonuses;
        set
        {
            _bonuses = value;
            OnPropertyChanged();
        }
    }
    private bool _useBonuses = false;
    public bool UseBonuses
    {
        get => _useBonuses;
        set
        {
            _useBonuses = value;
            OnPropertyChanged();
        }
    }

    public void RecalculateTotalPrice()
    {
        Bonuses = (UseBonuses && AuthService.IsAuthenticated)?
            _orderFacade.GetBonusSpend() 
          : _orderFacade.GetBonusEarn();
        
        TotalPrice = _orderFacade.GetFinalPrice(UseBonuses);
    }
    private RelayCommand _switchCommand;
    public RelayCommand SwitchCommand
    {
        get => _switchCommand ??= new RelayCommand((obj) => RecalculateTotalPrice());
    }
    public void CheckCompability()
    {
        var res = _orderFacade.CheckCompability();
        Errors = res.Item1.ToObservableCollection();
        IsCompatible = res.Item2;
        IsAnyErrors = IsCompatible || Errors.Any();
    }

    public CartViewModel(INavigationService navigationService, IComputerBuildSession session, 
        IServiceRepository services, OrderFacade orderFacade, IAuthService authService)
    {
        _navigationService = navigationService;
        ComputerSession = session;
        _orderFacade = orderFacade;
        AuthService = authService;

        AuthService.UserChanged += () => RecalculateTotalPrice();

        var allServices = services.GetAdditionalServices();
        foreach (var service in allServices)
            ServiceItems.Add(new ServiceSelectionViewModel(service, ComputerSession));
        ComputerSession.SelectedServicesChanged += () => RecalculateTotalPrice();

        Components = _orderFacade.GetComponentsDTO().ToObservableCollection();

        RecalculateTotalPrice();
        CheckCompability();
    }
}
