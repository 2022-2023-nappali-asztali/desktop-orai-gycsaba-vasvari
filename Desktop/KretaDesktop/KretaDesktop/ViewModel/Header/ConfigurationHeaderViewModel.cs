using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KretaDesktop.ViewModel.BaseClass;
using KretaDesktop.ViewModel.Configuration;

namespace KretaDesktop.ViewModel.Header
{
    public class ConfigurationHeaderViewModel : ViewModelBase
    {

        private ViewModelBase selectedView;
        public ViewModelBase SelectedView
        {
            get
            {
                return selectedView;
            }
            set 
            {
                selectedView= value;
                OnPropertyChanged(nameof(SelectedView));
            } 
        }

        public RelayCommand UpdateViewCommand { get; set;}

        public ConfigurationHeaderViewModel()
        {
            UpdateViewCommand = new RelayCommand(
                (parameter) => ChangeView(parameter)
            );
        }

        public void ChangeView(object viewName)
        {
            if (viewName != null)
            {
                if (viewName is string)
                {
                    switch (viewName)
                    {
                        case "SelectLanguage":
                            SelectedView= new LocalizationViewModel();
                            break;
                    }
                }
            }
        }
    }
}
