using ComputerConfiguration.Services.Navigation;
using ComputerConfiguration.ViewModels;
using System.Text;
using System.Windows;
namespace ComputerConfiguration;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{   
    public MainWindow(MainViewModel vm, NavigationStore navigationStore)
    {
        InitializeComponent();
        DataContext = vm;
    }
}