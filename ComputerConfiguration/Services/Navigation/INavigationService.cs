using ComputerConfiguration.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerConfiguration.Services.Navigation
{
    public interface INavigationService
    {
        void NavigateTo<TViewModel>(params object[] parameters) where TViewModel : ViewModelBase;
        void GoBack();
        bool CanGoBack { get; } 
    }
}
