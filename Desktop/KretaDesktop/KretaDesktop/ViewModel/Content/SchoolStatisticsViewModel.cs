using KretaCommandLine.Model;
using KretaDesktop.Services;
using KretaDesktop.ViewModel.BaseClass;
using System.Threading.Tasks;

namespace KretaDesktop.ViewModel.Content
{
    public class SchoolStatisticsViewModel : ServiceViewModelBase
    {
        public string _numberOfStudents;
        public string NumberOfStudents { get { return _numberOfStudents; } set { SetValue(ref _numberOfStudents, value); OnPropertyChanged(); } }

        public string _numberOfSubjects;
        public string NumberOfSubjects { get { return _numberOfSubjects; } set { SetValue(ref _numberOfSubjects, value); OnPropertyChanged(); } }

        public string _numberOfClasses;
        public string NumberOfClasses { get { return _numberOfClasses; } set { SetValue(ref _numberOfClasses, value); OnPropertyChanged(); } }

        public SchoolStatisticsViewModel(IAPIService service) : base(service)
        {
        }        

        public async override Task OnInitialize()
        {
            int _numberOfStudents = await _service.GetCountOf<Student>();
            NumberOfStudents =$"{ _numberOfStudents } fő";

            int _numberOfSubject = await _service.GetCountOf<Subject>();
            NumberOfSubjects = $"{_numberOfSubject} fő";

            int _numberOfClasses = await _service.GetCountOf<SchoolClass>();
            NumberOfClasses = $"{_numberOfClasses} fő";

            return;
        }
    }
}
