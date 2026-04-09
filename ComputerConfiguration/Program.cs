using ComputerConfiguration.Builder;
using ComputerConfiguration.DB;
using ComputerConfiguration.DTO;
using ComputerConfiguration.Filters;
using ComputerConfiguration.Models;
using ComputerConfiguration.Models.Catalog;
using ComputerConfiguration.Repositories;
using ComputerConfiguration.Services.Authentication;
using ComputerConfiguration.Services.Navigation;
using ComputerConfiguration.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace ComputerConfiguration
{
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services.AddSingleton<App>();

                    //бд
                    services.AddDbContext<ComputerConfigurationDBContext>();

                    //навигация
                    services.AddSingleton<NavigationStore>();
                    services.AddSingleton<INavigationTarget>(sp => sp.GetRequiredService<NavigationStore>());
                    services.AddSingleton<INavigationService, NavigationService>();
                    services.AddSingleton<IAuthService, AuthService>();

                    //
                    services.AddSingleton<IComputerBuildDtoBuilder, ComputerBuildDtoBuilder>();
                    services.AddSingleton<ComponentCatalog>();

                    services.AddSingleton<IServiceRepository, InMemoryServiceRepository>();

                    services.AddSingleton<IComponentFilterProvider, ComponentFilterProvider>();

                    //view и vm
                    services.AddSingleton<MainWindow>();
                    services.AddSingleton<MainViewModel>();
                    services.AddTransient<HomeViewModel>();
                    services.AddTransient<CatalogViewModel>();
                    services.AddTransient<CartViewModel>();
                    services.AddTransient<ComponentSelectionViewModel>();

                })
                .Build();

            var app = host.Services.GetService<App>();
            app?.Run();
        }
    }
}
