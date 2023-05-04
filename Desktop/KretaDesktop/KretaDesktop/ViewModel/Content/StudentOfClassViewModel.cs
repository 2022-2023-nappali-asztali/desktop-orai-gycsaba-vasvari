using KretaCommandLine.Model;
using KretaDesktop.Services;
using KretaDesktop.ViewModel.BaseClass;
using KretaDesktop.ViewModel.Command;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace KretaDesktop.ViewModel.Content
{
    public class StudentOfClassViewModel : ServiceViewModelBase<Student>
    {
        private IAPIService _service;
        private IStudentAPIService _studentAPIService;

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

        private Student _selectedStudentOfClass;

        public Student SelectedStudentOfClass
        {
            get { return _selectedStudentOfClass; }
            set { SetValue(ref _selectedStudentOfClass, value); }
        }


        private Student _selectedStudentHaveNoClass;
        public Student SelectedStudentHaveNoClass
        {
            get { return _selectedStudentHaveNoClass; }
            set { SetValue(ref _selectedStudentHaveNoClass, value); }
        }

        public StudentOfClassViewModel(IAPIService service, IStudentAPIService studentAPIService) : base(service)
        {
            _service = service;
            _studentAPIService = studentAPIService;

            _schoolClasses = new ObservableCollection<SchoolClass>();
            _studentsOfClass = new ObservableCollection<Student>();
            _studentsHaveNoClass = new ObservableCollection<Student>();

            AddStudentCommand = new AsyncRelayCommand(OnAddStudent, (ex) => OnException());
            RemoveStudentCommand = new AsyncRelayCommand(OnRemoveStudent, (ex) => OnException());
        }

        public AsyncRelayCommand AddStudentCommand { get; private set; }
        public AsyncRelayCommand RemoveStudentCommand { get; private set; }

        public async override Task OnInitialize()
        {
            if (SchoolClasses is object)
            {
                _schoolClasses.Clear();
                List<SchoolClass> schoolClasses = await _service.SelectAllRecordAsync<SchoolClass>();
                SchoolClasses = new ObservableCollection<SchoolClass>(schoolClasses);
            }
            await Refresh();
        }

        public async Task OnAddStudent()
        {
            if (SelectedStudentHaveNoClass is object && SelectedSchoolClass is object)
            {
                Student student = SelectedStudentHaveNoClass;
                student.SchoolClassId = SelectedSchoolClass.Id;
                await _service.Save<Student>(student);

                await Refresh();
            }
        }

        public async Task OnRemoveStudent()
        {
            if (SelectedStudentOfClass is object)
            {
                Student student = SelectedStudentOfClass;
                student.SchoolClassId = -1;
                await _service.Save<Student>(student);
                await Refresh();
            }
        }

        private async Task Refresh()
        {
            if (_selectedSchoolClass is object)
            {                
                _studentsOfClass.Clear();
                
                List<Student> sutdents= await _studentAPIService.SelectStudentOfClass<Student>(_selectedSchoolClass.Id);
                StudentsOfClass = new ObservableCollection<Student>(sutdents);

                _studentsHaveNoClass.Clear();
                List<Student> noClass= await _service.SelectAllByIdProperty<Student>("SchoolClassId", -1);
                StudentsHaveNoClass = new ObservableCollection<Student>(noClass);
            }
        }



        private void OnException()
        {

        }
    }
}
