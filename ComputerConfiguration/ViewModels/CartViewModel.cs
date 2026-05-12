using ComputerConfiguration.Builder;
using ComputerConfiguration.Commands;
using ComputerConfiguration.Converters;
using ComputerConfiguration.DTO;
using ComputerConfiguration.Models;
using ComputerConfiguration.Models.Build;
using ComputerConfiguration.Repositories.ServicesRepository;
using ComputerConfiguration.Services.Build;
using ComputerConfiguration.Services.Navigation;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace ComputerConfiguration.ViewModels;

public class CartViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    public IComputerBuildDtoBuilder ComputerBuilder;
    private readonly OrderFacade _orderFacade;
    public ObservableCollection<ServiceSelectionViewModel> ServiceItems { get; } = new();
    public ObservableCollection<ComponentCartDTO> Components { get; } = new();
    public ObservableCollection<CompabilityErrorDTO> Errors { get; } = new();
    public bool IsCompatible { get; set; }
    public bool IsAnyErrors { get; set; }

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

    public void RecalculateTotalPrice()
    {
        TotalPrice = _orderFacade.GetFinalPrice();
    }

    public CartViewModel(INavigationService navigationService, IComputerBuildDtoBuilder builder, 
        IServiceRepository services, OrderFacade orderFacade)
    {
        _navigationService = navigationService;
        ComputerBuilder = builder;
        _orderFacade = orderFacade;
        var allServices = services.GetAdditionalServices();
        foreach (var service in allServices)
            ServiceItems.Add(new ServiceSelectionViewModel(service, builder));
        ComputerBuilder.SelectedServicesChanged += () => RecalculateTotalPrice();
        RecalculateTotalPrice();
        Components = _orderFacade.GetComponentsDTO().ToObservableCollection();
        var res = _orderFacade.CheckCompability();
        Errors = res.Item1.ToObservableCollection();
        IsCompatible = !res.Item2;
        IsAnyErrors = IsCompatible || Errors.Any();
    }
}
