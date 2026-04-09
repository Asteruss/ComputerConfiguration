using ComputerConfiguration.Commands;
using ComputerConfiguration.Converters;
using ComputerConfiguration.DTO;
using ComputerConfiguration.Models;
using ComputerConfiguration.Models.Catalog;
using ComputerConfiguration.Models.Components;
using ComputerConfiguration.Services.Navigation;
using ComputerConfiguration.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;


namespace ComputerConfiguration.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        
        public RelayCommand GoToCatalogCommand { get; set; }
        public HomeViewModel(INavigationService navigationService, ComputerBuildDTO build, ComponentCatalog catalog)
        {
            GoToCatalogCommand = new((component) =>
            {
                navigationService.NavigateTo<CatalogViewModel>();
            });
        }
    }
}
