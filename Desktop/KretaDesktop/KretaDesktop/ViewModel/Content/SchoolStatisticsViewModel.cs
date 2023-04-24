using KretaCommandLine.Model;
using KretaDesktop.Services;
using KretaDesktop.ViewModel.BaseClass;
using System.Threading.Tasks;

namespace KretaDesktop.ViewModel.Content
{
    public class SchoolStatisticsViewModel : ServiceViewModelBase
    {
        public SchoolStatisticsViewModel(IAPIService service) : base(service)
        {
        }

        public string NumberOfStudents { get; set; }
        public string NumberOfSubjects { get; set; }
        public string NumberOfClasses { get; set; }

        public async override Task OnInitialize()
        {
            int _numberOfStudents = await _service.GetCountOf<Student>();
            NumberOfStudents =$"{ _numberOfStudents } fő";
            int _numberOfSubject = await _service.GetCountOf<Subject>();
            NumberOfStudents = $"{_numberOfSubject} fő";
            int _numberOfClasses = await _service.GetCountOf<SchoolClass>();
            NumberOfStudents = $"{_numberOfClasses} fő";

            return;
        }
    }
}
