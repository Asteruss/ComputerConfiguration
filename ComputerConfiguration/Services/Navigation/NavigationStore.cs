using ComputerConfiguration.Commands;
using ComputerConfiguration.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ComputerConfiguration.Services.Navigation
{
    public class NavigationStore : NotifyPropertyChanged, INavigationTarget
    {
        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }

       
    }
}
