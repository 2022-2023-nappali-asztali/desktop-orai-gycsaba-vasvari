using KretaCommandLine.DTO;
using KretaCommandLine.Model;
using KretaDesktop.Services;
using KretaDesktop.ViewModel.BaseClass;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KretaDesktop.ViewModel.Content
{
    public class SchoolClassStatisticsViewMoldel : ServiceViewModelBase
    {
        private List<NumberOfStudentInClass> _numberOfStudentInClasses;
        public List<NumberOfStudentInClass> NumberOfStudentInClasses
        {
            get { return _numberOfStudentInClasses; }
            set { SetValue(ref _numberOfStudentInClasses, value); }
        }

        private List<SchoolClass> _schoolClassWithNoStudent;
        public List<SchoolClass> SchoolClassWithNoStudent
        {
            get { return _schoolClassWithNoStudent; }
            set { SetValue(ref _schoolClassWithNoStudent,value); }
        }


        public SchoolClassStatisticsViewMoldel(IWrapService service) : base(service)
        {
        }

        public override async Task OnInitialize()
        {
            NumberOfStudentInClasses = await _service.NumberOfStudentPerClass();
            SchoolClassWithNoStudent= await _service.SchoolClassWithNoStudent();

        }
    }
}
