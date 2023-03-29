using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using KretaDesktop.ViewModel.BaseClass;
using KretaDesktop.ViewModel.Command;
using KretaDesktop.ViewModel.Header;
using Microsoft.Extensions.Logging;

namespace KretaDesktop.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        ILogger<MainWindowViewModel> _logger;
        private ConfigurationHeaderViewModel _configurationHeaderViewModel;
        private DataManagmentHeaderViewModel _dataMagmentHeaderViewModel;

        private ViewModelBase _selectedView;
        public ViewModelBase SelectedView
        {
            get { return _selectedView; }
            set
            {
                _selectedView = value;
                OnPropertyChanged(nameof(SelectedView));
            }
        }

        // a paraméter a dependency injection
        public MainWindowViewModel(
            ILogger<MainWindowViewModel> logger, 
            ConfigurationHeaderViewModel configurationHeaderViewModel,
            DataManagmentHeaderViewModel dataManagmentHeaderViewModel)
        {
            _logger = logger;
            //this.window = mainWindow;
            _configurationHeaderViewModel = configurationHeaderViewModel;
            _dataMagmentHeaderViewModel = dataManagmentHeaderViewModel;

            UpdateViewCommand = new RelayCommand((parameter) => UpdateView(parameter));
        }

        public RelayCommand UpdateViewCommand { get; }

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
                    _logger.LogInformation($"{nameof(MainWindowViewModel)} ->Konfigurációs menüpontot választotta");
                    SelectedView = _configurationHeaderViewModel;
                }
                else  if (commandParameter=="DataManagment")
                {
                    _logger.LogInformation($"{nameof(MainWindowViewModel)} - Adatkezelés menüpont választás");
                    SelectedView = _dataMagmentHeaderViewModel;
                }

            }
        }
    }
}
