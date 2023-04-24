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
using KretaDesktop.ViewModel.Content;
using KretaDesktop.View.Content;
using KretaDesktop.Services;

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
                    services.AddSingleton<IAPIService, APIService>();
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
                    services.AddSingleton<DataManagmentHeaderViewModel>();
                    services.AddSingleton<DataManagmentHeaderView>(
                        s => new DataManagmentHeaderView()
                        {
                            DataContext=s.GetRequiredService<DataManagmentHeaderViewModel>()
                        }
                    );
                    services.AddSingleton<DataManagmentHeaderViewModel>();
                    services.AddSingleton<DataManagmentHeaderView>(
                        s => new DataManagmentHeaderView()
                        {
                            DataContext = s.GetRequiredService<DataManagmentHeaderViewModel>()
                        }
                    );
                    services.AddSingleton<ListSubjectViewModel>();
                    services.AddSingleton<ListSubjectView>(
                        s => new ListSubjectView()
                        {
                            DataContext=s.GetRequiredService<ListSubjectViewModel>()
                        }
                    );

                    services.AddSingleton<ListStudentViewModel>();
                    services.AddSingleton<ListStudentView>(
                        s => new ListStudentView()
                        {
                            DataContext=s.GetRequiredService<ListStudentViewModel>()
                        }
                    );

                    services.AddSingleton<StudentByClassViewModel>();
                    services.AddSingleton<StudentByClassView>(
                        s => new StudentByClassView()
                        {
                            DataContext = s.GetRequiredService<StudentByClassViewModel>()
                        }
                    );
                    services.AddSingleton<SchoolStatisticsViewModel>();
                    services.AddSingleton<SchoolStatisticsView>(
                        s => new SchoolStatisticsView()
                        {
                            DataContext = s.GetRequiredService<SchoolStatisticsViewModel>()
                        }
                    );
                    services.AddSingleton<SchoolClassStatisticsViewMoldel>();
                    services.AddSingleton<SchoolClassStatisticsView>(
                        s => new SchoolClassStatisticsView()
                        {
                            DataContext = s.GetRequiredService<SchoolClassStatisticsView>()
                        }
                    );


                    services.AddSingleton<IAPIService, APIService>();
                    services.AddSingleton<IStudentAPIService, StudentAPIService>();

                    services.AddSingleton<IListStudentByClassViewModel,ListStudentByClassViewModel>();
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
