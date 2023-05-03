using KretaCommandLine.Model;
using KretaDesktop.Services;
using KretaDesktop.ViewModel.BaseClass;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace KretaDesktop.ViewModel.Content
{
    public class StudentOfClassViewModel : ServiceViewModelBase<Student>
    {
        private IAPIService _service;
        private IWrapService _wrapService;
        private IStudentAPIService _studentAPIService;

        private int _selectedSchoolClassIndex;
        public int SelectedSchoolClassIndex
        {
            get { return _selectedSchoolClassIndex; }
            set { _selectedSchoolClassIndex = value; }
        }


        private ObservableCollection<SchoolClass> _schoolClasses;
        public ObservableCollection<SchoolClass> SchoolClasses
        {
            get { return _schoolClasses; }
            set { SetValue(ref _schoolClasses, value); }
        }

        private ObservableCollection<Student> _studentsOfClass;
        public ObservableCollection<Student> StudentsOfClass
        {
            get { return _studentsOfClass; }
            set { SetValue(ref _studentsOfClass, value); }
        }

        private ObservableCollection<Student> _studentsHaveNoClass;
        public ObservableCollection<Student> StudentsHaveNoClass
        {
            get { return _studentsHaveNoClass; }
            set { SetValue(ref _studentsHaveNoClass, value); }
        }

        private SchoolClass _selectedSchoolClass;
        public SchoolClass SelectedSchoolClass
        {
            get { return _selectedSchoolClass; }
            set 
            {
                SetValue(ref _selectedSchoolClass, value);
                Refresh();
            }
        }

        private List<Student> _selectedStudentHaveNoClass;
        public List<Student> SelectedStudentHaveNoClass
        {
            get { return _selectedStudentHaveNoClass; }
            set { SetValue(ref _selectedStudentHaveNoClass, value); }
        }

        public StudentOfClassViewModel(IAPIService service, IWrapService wrapService, IStudentAPIService studentAPIService) : base(service)
        {
            _service = service;
            _wrapService = wrapService;
            _studentAPIService = studentAPIService;

            _schoolClasses = new ObservableCollection<SchoolClass>();
            _studentsOfClass = new ObservableCollection<Student>();
            _studentsHaveNoClass = new ObservableCollection<Student>();
        }

        public async override Task OnInitialize()
        {
            if (SchoolClasses is object)
            {
                //_schoolClasses.Clear();
                List<SchoolClass> schoolClasses = await _service.SelectAllRecordAsync<SchoolClass>();
                SchoolClasses = new ObservableCollection<SchoolClass>(schoolClasses);
            }
            await Refresh();
            //SelectedSchoolClassIndex = 0;
        }

        private async Task Refresh()
        {


            if (SelectedSchoolClass is object)
            {                
                //_studentsOfClass.Clear();
                List<Student> sutdents= await _studentAPIService.SelectStudentOfClass<Student>(_selectedSchoolClass.Id);
                StudentsOfClass = new ObservableCollection<Student>(sutdents);

                //_studentsHaveNoClass.Clear();
                List<Student> noClass= await _service.SelectAllByIdProperty<Student>("SchoolClassId", -1);
                StudentsHaveNoClass = new ObservableCollection<Student>(noClass);
            }
        }
    }
}
