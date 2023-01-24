using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using KretaDesktop.ViewModel.BaseClass;
using KretaDesktop.ViewModel.Header;
using Microsoft.Extensions.Logging;

namespace KretaDesktop.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        public RelayCommand UpdateViewCommand { get; }

        //private MainWindow window;

        private ViewModelBase selectedView;
        public ViewModelBase SelectedView
        {
            get { return selectedView; }
            set
            {
                selectedView = value;
                OnPropertyChanged(nameof(SelectedView));
            }
        }

        private ConfigurationHeaderViewModel configurationHeaderViewModel;
        private DataManagmentHeaderViewModel dataMagmentHeaderViewModel;

        ILogger<MainWindowViewModel> logger;

        // a paraméter a dependency injection
        public MainWindowViewModel(
            ILogger<MainWindowViewModel> logger, 
            ConfigurationHeaderViewModel configurationHeaderViewModel,
            DataManagmentHeaderViewModel dataManagmentHeaderViewModel)
        {
            this.logger = logger;
            //this.window = mainWindow;
            this.configurationHeaderViewModel = configurationHeaderViewModel;
            this.dataMagmentHeaderViewModel = dataManagmentHeaderViewModel;
            UpdateViewCommand = new RelayCommand((parameter) => UpdateView(parameter));
        }

        void UpdateView(object parameter)
        {
            if (parameter is string)
            {
                string commandParameter=(string)parameter;
                if (commandParameter == "Exit")
                {
                    //window.Close();
                    Application.Current.Shutdown();
                }
                else if (commandParameter=="Configuration")
                {
                    // Itt módosítjuk a View-t, készítünk egy BaseViewModel-ből öröklődő ViewModel-t
                    // Ennyi a menü választás
                    //SelectedView = new ConfigurationHeaderViewModel();
                    logger.LogInformation($"{nameof(MainWindowViewModel)} ->Konfigurációs menüpontot választotta");
                    SelectedView = configurationHeaderViewModel;
                }
                else  if (commandParameter=="DataMagment")
                {
                    logger.LogInformation($"{nameof(MainWindowViewModel)} - Adatkezelés menüpont választás");
                    SelectedView = dataMagmentHeaderViewModel;
                }

            }
        }
    }
}
