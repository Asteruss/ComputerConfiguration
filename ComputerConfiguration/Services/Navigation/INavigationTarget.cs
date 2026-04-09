using ComputerConfiguration.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerConfiguration.Services.Navigation
{
    public interface INavigationTarget
    {
        ViewModelBase CurrentViewModel { get; set; }
    }
}
