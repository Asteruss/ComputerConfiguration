using ComputerConfiguration.Builder;
using ComputerConfiguration.DB;
using ComputerConfiguration.Filters;
using ComputerConfiguration.Mappers;
using ComputerConfiguration.Repositories.ComponentRepository;
using ComputerConfiguration.Repositories.ServicesRepository;
using ComputerConfiguration.Services.Authentication;
using ComputerConfiguration.Services.Build;
using ComputerConfiguration.Services.Build.Compability;
using ComputerConfiguration.Services.Components;
using ComputerConfiguration.Services.Favorites;
using ComputerConfiguration.Services.Navigation;
using ComputerConfiguration.Services.Payment;
using ComputerConfiguration.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace ComputerConfiguration;

public partial class App : Application
{
    private readonly IHost _host;

    public App()
    {
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.AddDbContext<ComputerConfigurationDBContext>();
                services.AddDbContext<LocalDBContext>();
                services.AddDbContextFactory<ComputerConfigurationDBContext>();
                services.AddDbContextFactory<LocalDBContext>();

                services.AddSingleton<NavigationStore>();
                services.AddSingleton<INavigationTarget>(sp => sp.GetRequiredService<NavigationStore>());
                services.AddSingleton<INavigationService, NavigationService>();

                services.AddSingleton<IComputerBuildMapper, ComputerBuildMapper>();
                services.AddSingleton<IComputerBuildDtoBuilder, ComputerBuildDtoBuilder>();
                services.AddSingleton<IComputerBuildSession, ComputerBuildSession>();


                services.AddSingleton<IServiceRepository, DbServiceRepository>();
                services.AddSingleton<IComponentFilterProvider, ComponentFilterProvider>();
                services.AddSingleton<IComponentRepository, DBComponentRepository>();
                services.AddSingleton<IComponentFactory, ComponentFactory>();


                // services
                services.AddSingleton<IAuthService, AuthService>();
                services.AddSingleton<IAddressService, AddressService>();
                services.AddSingleton<BonusService>();
                services.AddSingleton<PricingService>();
                services.AddSingleton<OrderService>();
                services.AddSingleton<IPaymentService, FakePaymentService>();
                services.AddSingleton<ICompatibilityRuleFactory, CompatibilityRuleFactory>();
                services.AddSingleton(provider =>
                {
                    var factory = provider.GetRequiredService<ICompatibilityRuleFactory>();
                    var rules = factory.CreateRules();
                    return new CompatibilityCheckerService(rules);
                });
                services.AddSingleton<OrderFacade>();

                services.AddSingleton<FavoriteFacade>();
                services.AddSingleton<IComponentEnricher, ComponentEnricher>();

                // ViewModels и окна
                services.AddSingleton<MainWindow>();
                services.AddSingleton<MainViewModel>();
                services.AddTransient<HomeViewModel>();
                services.AddTransient<CartViewModel>();
                services.AddTransient<RegistrationViewModel>();
                services.AddTransient<LoginViewModel>();
                services.AddTransient<ComponentSelectionViewModel>();
                services.AddTransient<AddressAddingViewModel>();
                services.AddTransient<AddressListViewModel>();
                services.AddTransient<OrderViewModel>();
                services.AddTransient<OrderTableViewModel>();
            })
            .Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        await _host.StartAsync();

        var mainWindow = _host.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await _host.StopAsync();
        _host.Dispose();
        base.OnExit(e);
    }
}