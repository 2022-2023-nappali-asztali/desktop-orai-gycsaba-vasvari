using KretaCommandLine.Model;
using KretaDesktop.Services;
using KretaDesktop.ViewModel.BaseClass;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace KretaDesktop.ViewModel.Content
{
    public class StudentByClassViewModel : ServiceViewModelBase<SchoolClass>
    {
        private ObservableCollection<SchoolClass> _schoolClasses;
        public ObservableCollection<SchoolClass> SchoolClasses
        {
            get => _schoolClasses;
            set => SetValue(ref _schoolClasses, value);
        }

        private SchoolClass _selectedSchoolClass;
        public SchoolClass SelectedSchoolClass
        {
            get => _selectedSchoolClass;
            set
            {
                SetValue(ref _selectedSchoolClass, value);
                StudentViewModel.OnInitialize(SelectedSchoolClass);
            }
        }

        public IListStudentByClassViewModel StudentViewModel { get; set; }

        public StudentByClassViewModel(IAPIService service, IListStudentByClassViewModel studentViewModel) : base(service)
        {
            SchoolClasses = new ObservableCollection<SchoolClass>();
            StudentViewModel = studentViewModel;
        }

        public override async Task OnInitialize()
        {
            SchoolClasses.Clear();
            List<SchoolClass> schoolClasses = await _service.SelectAllRecordAsync<SchoolClass>();
            SchoolClasses = new ObservableCollection<SchoolClass>(schoolClasses);
        }
    }
}
