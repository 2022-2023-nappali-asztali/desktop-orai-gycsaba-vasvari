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
        TeachTeacherSubjectViewModel _teachTeacherSubjectViewModel;
        StudentOfClassViewModel _studentOfClassViewModel;

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
            TeachTeacherSubjectViewModel teachTeacherSubjectViewModel,
            StudentOfClassViewModel studentOfClassViewModel
            )
        {
            _teachTeacherSubjectViewModel = teachTeacherSubjectViewModel;
            _studentOfClassViewModel = studentOfClassViewModel;

            UpdateViewCommand = new AsyncRelayCommandWithParameter((parameter) => ChangeView(parameter), (ex) => OnException());
        }

        public AsyncRelayCommandWithParameter UpdateViewCommand { get; set; }

        private async Task ChangeView(object parameter)
        {
            if (parameter != null && parameter is string)
            {
                switch (parameter)
                {
                    case "TeacherSubjects":
                        SelectedView = _teachTeacherSubjectViewModel;
                        await _teachTeacherSubjectViewModel.OnInitialize();
                        break;
                    case "SchoolClassStudents":
                        SelectedView = _studentOfClassViewModel;
                        await _studentOfClassViewModel.OnInitialize();
                        break;

                }
            }
        }

        private void OnException()
        {
        }
    }
}
