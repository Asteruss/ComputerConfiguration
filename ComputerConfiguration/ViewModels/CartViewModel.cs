using ComputerConfiguration.Builder;
using ComputerConfiguration.Commands;
using ComputerConfiguration.Converters;
using ComputerConfiguration.Models.Build;
using ComputerConfiguration.Repositories.ServicesRepository;
using ComputerConfiguration.Services.Navigation;
using System.Collections.ObjectModel;
using System.Windows;

namespace ComputerConfiguration.ViewModels
{
    public class CartViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public IComputerBuildDtoBuilder ComputerBuild;
        public ObservableCollection<AdditionalService> AllServices { get; set; }
        //private RelayCommand _buyCommand;
        //public RelayCommand BuyCommand
        //{
        //    get => _buyCommand ?? new RelayCommand((obj) =>
        //    {

        //    });
        //}

        private RelayCommand _rebuildOnCheckCommand;
        public RelayCommand RebuildOnCheckCommand
        {
            get => _rebuildOnCheckCommand ?? (_rebuildOnCheckCommand = new RelayCommand(option =>
            {
                if (option is AdditionalServiceOption opt)
                    ComputerBuild.AddAdditionalService(opt);
            }));
        }
        private RelayCommand _rebuildOnUncheckCommand;
        public RelayCommand RebuildOnUncheckCommand
        {
            get => _rebuildOnUncheckCommand ?? (_rebuildOnUncheckCommand = new RelayCommand(option =>
            {
                if (option is AdditionalServiceOption opt)
                    ComputerBuild.RemoveAdditionalService(opt);
            }));
        }
        public CartViewModel(INavigationService navigationService, IComputerBuildDtoBuilder builder, IServiceRepository services)
        {
            _navigationService = navigationService;
            ComputerBuild = builder;
            AllServices = services.GetAdditionalServices().ToObservableCollection();
        }
    }
}
