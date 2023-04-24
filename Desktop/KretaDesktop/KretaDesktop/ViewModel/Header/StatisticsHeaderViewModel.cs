using KretaDesktop.View.Content;
using KretaDesktop.ViewModel.BaseClass;
using KretaDesktop.ViewModel.Command;
using KretaDesktop.ViewModel.Content;
using System.Threading.Tasks;

namespace KretaDesktop.ViewModel.Header
{
    public class StatisticsHeaderViewModel : ViewModelBase
    {
        SchoolStatisticsViewModel _schoolStatisticsViewModel;
        SchoolClassStatisticsViewMoldel _schoolClassStatisticsViewMoldel;

        private InitializedViewModelBase selectedView;
        public InitializedViewModelBase SelectedView
        {
            get { return selectedView; }
            set
            {
                selectedView = value;
                OnPropertyChanged();
            }
        }

        public StatisticsHeaderViewModel(
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

