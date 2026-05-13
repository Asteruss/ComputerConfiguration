using ComputerConfiguration.Builder;
using ComputerConfiguration.Commands;
using ComputerConfiguration.DTO;
using ComputerConfiguration.Models;
using ComputerConfiguration.Models.Components;
using ComputerConfiguration.Models.Enums;
using ComputerConfiguration.Repositories.ComponentRepository;
using ComputerConfiguration.Services.Authentication;
using ComputerConfiguration.Services.Navigation;
using ComputerConfiguration.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace ComputerConfiguration.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public NavigationStore NavigationStore { get; }
        private readonly IComponentRepository _catalog;
        private readonly IComputerBuildDtoBuilder _builder;
        private readonly INavigationService _navigationService;
        public IAuthService AuthService { get; init; }
        public ComputerBuildDTO ComputerBuild => _builder.BuildDto();
        #region команды перехода к компонентам
        private RelayCommand _goToCpuCatalogCommand;
        public RelayCommand GoToCpuCatalogCommand
        {
            get => _goToCpuCatalogCommand ??= new((obj) =>
            _navigationService.NavigateTo<ComponentSelectionViewModel>(_catalog.GetCpus(), ComponentCategory.CPU));
        }
        private RelayCommand _goToGpuCatalogCommand;
        public RelayCommand GoToGpuCatalogCommand
        {
            get => _goToGpuCatalogCommand ??= new((obj) =>
            _navigationService.NavigateTo<ComponentSelectionViewModel>(_catalog.GetGpus(), ComponentCategory.GPU));
        }
        private RelayCommand _goToMotherboardCatalogCommand;
        public RelayCommand GoToMotherboardCatalogCommand
        {
            get => _goToMotherboardCatalogCommand ??= new((obj) =>
            _navigationService.NavigateTo<ComponentSelectionViewModel>(_catalog.GetMotherboards(), ComponentCategory.Motherboard));
        }
        private RelayCommand _goToRamCatalogCommand;
        public RelayCommand GoToRamCatalogCommand
        {
            get => _goToRamCatalogCommand ??= new((obj) =>
            _navigationService.NavigateTo<ComponentSelectionViewModel>(_catalog.GetRam(), ComponentCategory.RAM));
        }
        private RelayCommand _goToStorageCatalogCommand;
        public RelayCommand GoToStorageCatalogCommand
        {
            get => _goToStorageCatalogCommand ??= new((obj) =>
            _navigationService.NavigateTo<ComponentSelectionViewModel>(_catalog.GetStorages(), ComponentCategory.Storage));
        }
        private RelayCommand _goToPsuCatalogCommand;
        public RelayCommand GoToPsuCatalogCommand
        {
            get => _goToPsuCatalogCommand ??= new((obj) =>
            _navigationService.NavigateTo<ComponentSelectionViewModel>(_catalog.GetPsu(), ComponentCategory.PSU));
        }
        private RelayCommand _goToCoolerCatalogCommand;
        public RelayCommand GoToCoolerCatalogCommand
        {
            get => _goToCoolerCatalogCommand ??= new((obj) =>
            _navigationService.NavigateTo<ComponentSelectionViewModel>(_catalog.GetCoolers(), ComponentCategory.Cooler));
        }
        private RelayCommand _goToCaseCatalogCommand;
        public RelayCommand GoToCaseCatalogCommand
        {
            get => _goToCaseCatalogCommand ??= new((obj) =>
            _navigationService.NavigateTo<ComponentSelectionViewModel>(_catalog.GetCases(), ComponentCategory.Case));
        }
        #endregion
        private RelayCommand _goToCartCommand;
        public RelayCommand GoToCartCommand
        {
            get => _goToCartCommand ?? (_goToCartCommand = new((obj) =>
            _navigationService.NavigateTo<CartViewModel>()));
        }

        private bool _isMenuOpen = false;
        public bool IsMenuOpen
        {
            get => _isMenuOpen;
            set
            {
                _isMenuOpen = value;
                OnPropertyChanged();
            }
        }
        #region Ауентификация
        private RelayCommand _goToRegisterCommand;
        public RelayCommand GoToRegisterCommand
        {
            get => _goToRegisterCommand ??= new((obj) =>
            {
                _navigationService.NavigateTo<RegistrationViewModel>();
                IsMenuOpen = false;
            });
        }
        private RelayCommand _goToLoginCommand;
        public RelayCommand GoToLoginCommand
        {
            get => _goToLoginCommand ??= new((obj) =>
            {
                _navigationService.NavigateTo<LoginViewModel>();
                IsMenuOpen = false;
            });
        }
        private RelayCommand _logoutCommand;
        public RelayCommand LogoutCommand
        {
            get => _logoutCommand ??= new((obj) =>
            {
                AuthService.Logout();
                IsMenuOpen = false;
            });
        }
        #endregion
        public MainViewModel(INavigationService navigationService, NavigationStore navigationStore, 
            IComputerBuildDtoBuilder builder, IComponentRepository componets, IAuthService authService)
        {
            navigationService.NavigateTo<HomeViewModel>();
            NavigationStore = navigationStore;
            _navigationService = navigationService;
            _builder = builder;
            _catalog = componets;
            AuthService = authService;
        }
    }
}
