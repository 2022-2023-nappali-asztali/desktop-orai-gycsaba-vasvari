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

namespace KretaDesktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost host;
        ILogger logger;

        public App()
        {
            logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File($"log\\log.txt")
                .CreateLogger();

            logger.Information("Appplication is started...");

            host = Host.CreateDefaultBuilder()
                .UseSerilog()
                .ConfigureServices(services =>
                {
                    services.AddSingleton<MainWindowViewModel>();
                    services.AddSingleton<MainWindow>(s => new MainWindow()
                    {
                        DataContext = s.GetRequiredService<MainWindowViewModel>()
                    });


                    
                })
                .Build();
        }

        protected async override void OnStartup(StartupEventArgs e)
        {
            await host.StartAsync();
            try
            {
                var window = host.Services.GetRequiredService<MainWindow>();
                window.Show();
            }
            catch(Exception ex)
            {
                logger.Information($"{ex.Message}");
            }
            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await host.StopAsync();
            host.Dispose();

            base.OnExit(e);
        }




    }
}
