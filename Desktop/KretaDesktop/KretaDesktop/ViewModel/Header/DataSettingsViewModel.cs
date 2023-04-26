using KretaDesktop.ViewModel.BaseClass;
using KretaDesktop.ViewModel.Command;
using KretaDesktop.ViewModel.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KretaDesktop.ViewModel.Header
{
    public class DataSettingsViewModel : ViewModelBase
    {
        SchoolStatisticsViewModel _schoolStatisticsViewModel;
        SchoolClassStatisticsViewMoldel _schoolClassStatisticsViewMoldel;

        private InitializedViewModelBase _selectedView;
        public InitializedViewModelBase SelectedView
        {
            get { return _selectedView; }
            set
            {
                _selectedView = value;
                OnPropertyChanged();
            }
        }

        public DataSettingsViewModel(
            SchoolStatisticsViewModel schoolStatisticsViewModel,
            SchoolClassStatisticsViewMoldel schoolClassStatisticsViewMoldel
            )
        {
            _schoolStatisticsViewModel = schoolStatisticsViewModel;
            _schoolClassStatisticsViewMoldel = schoolClassStatisticsViewMoldel;

            UpdateViewCommand = new AsyncRelayCommandWithParameter((parameter) => ChangeView(parameter), (ex) => OnException());
        }

        public AsyncRelayCommandWithParameter UpdateViewCommand { get; set; }

        private async Task ChangeView(object parameter)
        {
            if (parameter != null && parameter is string)
            {
                switch (parameter)
                {
                    case "SchoolStatistics":
                        SelectedView = _schoolStatisticsViewModel;
                        await _schoolStatisticsViewModel.OnInitialize();
                        break;
                    case "SchoolClassStatistics":
                        SelectedView = _schoolClassStatisticsViewMoldel;
                        await _schoolClassStatisticsViewMoldel.OnInitialize();
                        break;

                }
            }
        }

        private void OnException()
        {
        }
    }
}
