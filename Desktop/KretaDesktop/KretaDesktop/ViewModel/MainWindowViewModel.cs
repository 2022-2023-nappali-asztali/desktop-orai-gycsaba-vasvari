using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KretaDesktop.ViewModel.BaseClass;
using KretaDesktop.ViewModel.Header;

namespace KretaDesktop.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        public RelayCommand UpdateViewCommand { get; }

        private MainWindow window;

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

        public MainWindowViewModel(MainWindow window)
        {
            this.window = window;
            UpdateViewCommand = new RelayCommand((parameter) => UpdateView(parameter));
        }

        void UpdateView(object parameter)
        {
            if (parameter is string)
            {
                string commandParameter=(string)parameter;
                if (commandParameter == "Exit")
                {
                   window.Close();
                }
                else if (commandParameter=="Configuration")
                {
                    // Itt módosítjuk a View-t, készítünk egy BaseViewModel-ből öröklődő ViewModel-t
                    // Ennyi a menü választás
                    SelectedView = new ConfigurationHeaderViewModel();
                }
            }
        }
    }
}
