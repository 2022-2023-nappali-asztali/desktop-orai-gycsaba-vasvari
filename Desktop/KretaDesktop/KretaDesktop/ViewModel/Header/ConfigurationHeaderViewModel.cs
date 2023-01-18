using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KretaDesktop.ViewModel.BaseClass;
using KretaDesktop.ViewModel.Configuration;
using Microsoft.Extensions.Logging;

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

        private ILogger<ConfigurationHeaderViewModel> logger;
        private LocalizationViewModel localizationView;

        public ConfigurationHeaderViewModel(ILogger<ConfigurationHeaderViewModel> logger, LocalizationViewModel localizationViewModel)
        {
            this.logger = logger;
            this.localizationView = localizationViewModel;
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
                            //SelectedView= new LocalizationViewModel();
                            logger.LogInformation($"{nameof(ConfigurationHeaderViewModel)} -> A nyelv választás menüpontot választotta");
                            SelectedView= localizationView;
                            break;
                    }
                }
            }
        }
    }
}
