using ComputerConfiguration.Builder;
using ComputerConfiguration.Commands;
using ComputerConfiguration.DTO;
using ComputerConfiguration.Models;
using ComputerConfiguration.Models.Catalog;
using ComputerConfiguration.Models.Components;
using ComputerConfiguration.Models.Enums;
using ComputerConfiguration.Services.Navigation;
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
        private readonly IComputerBuildDtoBuilder _builder;
        private readonly ComponentCatalog _catalog;
        private readonly INavigationService _navigationService;
        public ComputerBuildDTO ComputerBuild => _builder.BuildDto();

        private RelayCommand _goToCpuCatalogCommand;
        public RelayCommand GoToCpuCatalogCommand { get => _goToCpuCatalogCommand ?? (_goToCpuCatalogCommand = new((obj) =>
            _navigationService.NavigateTo<ComponentSelectionViewModel>(_catalog.Cpus, ComponentCategory.CPU))); }
        private RelayCommand _goToGpuCatalogCommand;
        public RelayCommand GoToGpuCatalogCommand
        {
            get => _goToGpuCatalogCommand ?? (_goToGpuCatalogCommand = new((obj) =>
            _navigationService.NavigateTo<ComponentSelectionViewModel>(_catalog.Gpus, ComponentCategory.GPU)));
        }
        private RelayCommand _goToCartCommand;
        public RelayCommand GoToCartCommand
        {
            get => _goToCartCommand ?? (_goToCartCommand = new((obj) =>
            _navigationService.NavigateTo<CartViewModel>()));
        }
        public MainViewModel(INavigationService navigationService, NavigationStore navigationStore, IComputerBuildDtoBuilder builder, ComponentCatalog catalog)
        {
            navigationService.NavigateTo<HomeViewModel>();
            NavigationStore = navigationStore;
            _navigationService = navigationService;
            _builder = builder;
            _catalog = catalog;
        }
    }
}
