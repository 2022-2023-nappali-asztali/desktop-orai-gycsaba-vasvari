using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KretaDesktop.ViewModel.BaseClass;

namespace KretaDesktop.ViewModel
{
    class MainWindowViewModel
    {
        public RelayCommand UpdateViewCommand { get; }

        private MainWindow window;

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
            }
        }
    }
}
