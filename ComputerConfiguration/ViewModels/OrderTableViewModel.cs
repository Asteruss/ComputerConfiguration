using ComputerConfiguration.Converters;
using ComputerConfiguration.Models.Orders;
using ComputerConfiguration.Services.Authentication;
using ComputerConfiguration.Services.Build;
using ComputerConfiguration.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerConfiguration.ViewModels;

public class OrderTableViewModel : ViewModelBase
{
    public ObservableCollection<Order> Orders { get; set; }
    public OrderTableViewModel(OrderService orderService, IAuthService authService, INavigationService navigation)
    {
        if (!authService.IsAuthenticated)
            navigation.GoBack();
        Orders = orderService.GetOrders(authService.CurrentUser!.Id).ToObservableCollection();
    }
}
