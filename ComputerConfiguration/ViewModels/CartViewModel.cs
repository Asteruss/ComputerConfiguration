using ComputerConfiguration.Builder;
using ComputerConfiguration.Commands;
using ComputerConfiguration.Converters;
using ComputerConfiguration.DTO;
using ComputerConfiguration.Models.Build;
using ComputerConfiguration.Models.Build.Decorators;
using ComputerConfiguration.Repositories;
using ComputerConfiguration.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerConfiguration.ViewModels
{
    public class CartViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public IBuild ComputerBuild;
        public IBuild CurrentBuild;
        public ObservableCollection<AdditionalService> AllServices { get; set; }
        private RelayCommand _buyCommand;
        public RelayCommand BuyCommand
        {
            get => _buyCommand ?? new RelayCommand((obj) =>
            {

            });
        }
        private bool _isWarrantySelected;
        public bool IsWarrantySelected
        {
            get => _isWarrantySelected;
            set
            {
                _isWarrantySelected = value;
                Rebuild();
                OnPropertyChanged();
            }
        }

        private int _warrantyYears = 1;
        public int WarrantyYears
        {
            get => _warrantyYears;
            set
            {
                _warrantyYears = value;
                Rebuild();
                OnPropertyChanged();
            }
        }
        private bool _isCableManagementSelected;
        public bool IsCableManagementSelected
        {
            get => _isCableManagementSelected;
            set
            {
                _isCableManagementSelected = value;
                OnPropertyChanged();
                Rebuild();
            }
        }

        private string _cableManagementLevel = "Стандартная";
        public string CableManagementLevel
        {
            get => _cableManagementLevel;
            set
            {
                _cableManagementLevel = value;
                OnPropertyChanged();
                Rebuild();
            }
        }
        private bool _isOsInstallerSelected;
        public bool IsOsInstallerSelected
        {
            get => _isOsInstallerSelected;
            set
            {
                _isOsInstallerSelected = value;
                OnPropertyChanged();
                Rebuild();
            }
        }
        private AdditionalService _osServiceSelected;
        public AdditionalService OsServiceSelected
        {
            get => _osServiceSelected ?? AllServices[0];
            set
            {
                _osServiceSelected = value;
                OnPropertyChanged();
                Rebuild();
            }
        }
        private AdditionalServiceOption _osOptionSelected;
        public AdditionalServiceOption OsOptionSelected
        {
            get => _osOptionSelected ?? AllServices[0].AdditionalServiceOptions[0];
            set
            {
                _osOptionSelected = value;
                OnPropertyChanged();
                Rebuild();
            }
        }
        private double GetCableManagementPrice(string level) => level switch
        {
            "Стандартная" => 0,
            "Черные кабели" => 200,
            "Полная кастомизация" => 300,
            _ => 0
        };
        public double TotalPrice => CurrentBuild.GetTotalPrice();
        public string Description => CurrentBuild.GetDescription();
        private void Rebuild()
        {
            IBuild build = ComputerBuild;
            if (IsWarrantySelected)
                build = new WarrantyDecorator(build, WarrantyYears, 200);
            if (IsCableManagementSelected)
            {
                double price = GetCableManagementPrice(CableManagementLevel);
                build = new CableManagementDecorator(build, CableManagementLevel, price);
            }
            if (_isOsInstallerSelected)
            {
                build = new OSInstallDecorator(build, OsServiceSelected, OsOptionSelected);
            }
            build = new BulkDiscountDecorator(build, 3000, 5);
            CurrentBuild = build;
            OnPropertyChanged(nameof(ComputerBuild));
            OnPropertyChanged(nameof(TotalPrice));
            OnPropertyChanged(nameof(Description));
        }
        public CartViewModel(INavigationService navigationService, IComputerBuildDtoBuilder builder, IServiceRepository services)
        {
            _navigationService = navigationService;
            ComputerBuild = builder.BuildFinal();
            CurrentBuild = ComputerBuild;
            AllServices = services.GetAdditionalServices().ToObservableCollection();
            Rebuild();
        }
    }
}
