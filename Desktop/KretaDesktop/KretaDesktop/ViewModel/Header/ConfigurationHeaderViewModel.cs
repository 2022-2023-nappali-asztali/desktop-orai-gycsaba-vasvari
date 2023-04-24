using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KretaDesktop.ViewModel.BaseClass;
using KretaDesktop.ViewModel.Command;
using KretaDesktop.ViewModel.Configuration;
using Microsoft.Extensions.Logging;

namespace KretaDesktop.ViewModel.Header
{
    public class ConfigurationHeaderViewModel : ViewModelBase
    {
        private readonly ILogger<ConfigurationHeaderViewModel> _logger;
        private LocalizationViewModel _localizationView;

        private ViewModelBase _selectedView;
        public ViewModelBase SelectedView
        {
            get
            {
                return _selectedView;
            }
            set 
            {
                _selectedView= value;
                OnPropertyChanged(nameof(SelectedView));
            } 
        }

        public RelayCommand UpdateViewCommand { get; set;}

        public ConfigurationHeaderViewModel(ILogger<ConfigurationHeaderViewModel> logger, LocalizationViewModel localizationViewModel)
        {
            this._logger = logger;
            this._localizationView = localizationViewModel;
            UpdateViewCommand = new RelayCommand(
                (parameter) => ChangeView(parameter)
            );
        }

        private void ChangeView(object viewName)
        {
            if (viewName != null)
            {
                if (viewName is string)
                {
                    switch (viewName)
                    {
                        case "SelectLanguage":
                            //SelectedView= new LocalizationViewModel();
                            _logger.LogInformation($"{nameof(ConfigurationHeaderViewModel)} -> A nyelv választás menüpontot választotta");
                            SelectedView= _localizationView;
                            break;
                    }
                }
            }
        }
    }
}
