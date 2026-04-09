using ComputerConfiguration.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ComputerConfiguration.Services.Navigation
{
    public class NavigationService : INavigationService
    {
        private readonly INavigationTarget _navigationTarget;
        private readonly IServiceProvider _serviceProvider;
        private readonly Stack<ViewModelBase> _history = new();
        public NavigationService(INavigationTarget navigationTarget, IServiceProvider serviceProvider)
        {
            _navigationTarget = navigationTarget;
            _serviceProvider = serviceProvider;
        }

        public void NavigateTo<TViewModel>(params object[] parameters) where TViewModel : ViewModelBase
        {
            if (_navigationTarget.CurrentViewModel != null)
                _history.Push(_navigationTarget.CurrentViewModel);
            //_navigationTarget.CurrentViewModel = _serviceProvider.GetRequiredService<TViewModel>();
            _navigationTarget.CurrentViewModel = ActivatorUtilities.CreateInstance<TViewModel>(_serviceProvider, parameters);
        }
        public void GoBack()
        {
            if (CanGoBack) 
                _navigationTarget.CurrentViewModel = _history.Pop();   
        }

        public bool CanGoBack => _history.Count > 0;
    }
}
