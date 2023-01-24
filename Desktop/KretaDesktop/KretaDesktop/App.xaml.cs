using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;


using Microsoft.Extensions.Hosting;
using Serilog;

using Microsoft.Extensions.DependencyInjection;
using KretaDesktop.ViewModel;
using KretaDesktop.View.Header;
using KretaDesktop.ViewModel.Header;
using KretaDesktop.ViewModel.Configuration;
using KretaDesktop.View.Configuration;
using KretaDesktop.Localization;

namespace KretaDesktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost host;
        //ILogger logger;

        public App()
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File($"log\\log.txt")
                .CreateLogger();

            Log.Logger.Information("Applikáció elinudlt...");

            host = Host.CreateDefaultBuilder()
                .UseSerilog()
                .ConfigureServices(services =>
                {
                    services.AddSingleton<MainWindowViewModel>();
                    services.AddSingleton<MainWindow>(s => new MainWindow()
                    {
                        DataContext = s.GetRequiredService<MainWindowViewModel>()
                    });
                    services.AddSingleton<ConfigurationHeaderViewModel>();
                    services.AddSingleton<ConfigurationHeaderView>(
                        service => new ConfigurationHeaderView()
                        {
                            DataContext= service.GetRequiredService<ConfigurationHeaderViewModel>()
                        }
                    );
                    services.AddSingleton<LocalizationViewModel>();
                    services.AddSingleton<LocalizationView>(
                        s => new LocalizationView()
                        {
                            DataContext=s.GetRequiredService<LocalizationViewModel>()
                        }
                    );
                    services.AddSingleton<DataMagmentHeaderViewModel>();
                    services.AddSingleton<DataManagmentHeaderView>(
                        s => new DataManagmentHeaderView()
                        {
                            DataContext=s.GetRequiredService<DataMagmentHeaderViewModel>
                        }
                    );
                })
                .Build();
            Log.Logger.Information("Build megtörtént...");
        }

        protected async override void OnStartup(StartupEventArgs e)
        {
            await host.StartAsync();
            try
            {
                ProjectLocalization projectLocalization = new ProjectLocalization();
                projectLocalization.SwitchToCurrentCuture();
                var window = host.Services.GetRequiredService<MainWindow>();
                window.Show();
                Log.Logger.Information("Ablak megjelent...");
            }
            catch(Exception ex)
            {
                Log.Logger.Information($"{ex.Message}");
            }
            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await host.StopAsync();
            host.Dispose();
            Log.Logger.Information("Kilépés az applikációból...");
            base.OnExit(e);
        }




    }
}
