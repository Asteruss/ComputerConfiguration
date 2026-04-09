using ComputerConfiguration.Builder;
using ComputerConfiguration.Commands;
using ComputerConfiguration.Converters;
using ComputerConfiguration.DTO;
using ComputerConfiguration.Models;
using ComputerConfiguration.Models.Catalog;
using ComputerConfiguration.Models.Components;
using ComputerConfiguration.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerConfiguration.ViewModels
{
    public class CatalogViewModel : ViewModelBase
    {
        private readonly IComputerBuildDtoBuilder _builder;
        private readonly INavigationService _navigationService;
        public ComputerBuildDTO CurrentBuild => _builder.BuildDto();

        public ObservableCollection<Cpu> Cpus { get; set; }
        public ObservableCollection<Gpu> Gpus { get; set; }
        public ObservableCollection<Motherboard> Motherboards { get; set; }
        public ObservableCollection<Ram> Rams { get; set; }
        public RelayCommand SelectComponentCommand { get; set; }
        private RelayCommand _returnCommand;
        public RelayCommand ReturnCommand
        {
            get => _returnCommand ?? new((obj) => { if (_navigationService.CanGoBack) _navigationService.NavigateTo<CartViewModel>(); });
        }
        private RelayCommand _toCartCommand;
        public RelayCommand ToCartCommand
        {
            get => _toCartCommand ?? new((obj) => _navigationService.NavigateTo<CartViewModel>());
        }
        public CatalogViewModel(INavigationService navigationService, IComputerBuildDtoBuilder builder, ComponentCatalog catalog)
        {
            _navigationService = navigationService;
            Cpus = catalog.Cpus.ToObservableCollection();
            Gpus = catalog.Gpus.ToObservableCollection();
            Motherboards = catalog.Motherboards.ToObservableCollection();
            Rams = catalog.Rams.ToObservableCollection();
            _builder = builder;
            SelectComponentCommand = new((component) =>
            {
                if (component is Cpu cpu)
                    builder.SetCpu(cpu);
                if (component is Gpu gpu)
                    builder.SetGpu(gpu);
                if (component is Motherboard mot)
                    builder.SetMotherboard(mot);
                if (component is Ram ram)
                    _builder.AddRam(ram);
                OnPropertyChanged(nameof(CurrentBuild));
            });
        }

    }
}
