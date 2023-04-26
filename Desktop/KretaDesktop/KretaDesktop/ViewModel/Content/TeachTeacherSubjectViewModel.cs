using KretaCommandLine.Model;
using KretaDesktop.Services;
using KretaDesktop.ViewModel.BaseClass;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KretaDesktop.ViewModel.Content
{
    public class TeachTeacherSubjectViewModel : ServiceViewModelBase<Teacher>
    {
        private IAPIService _service;            
        private List<Teacher> _teachers;

        public List<Teacher> Teachers
        {
            get { return _teachers; }
            set { SetValue(ref _teachers, value); }
        }

        private Teacher _selectedTeacher;

        public TeachTeacherSubjectViewModel(IAPIService service) : base(service)
        {
            _service = service;
        }

        public Teacher SelectedTeacher
        {
            get { return _selectedTeacher; }
            set { SetValue(ref _selectedTeacher, value); }
        }

        public async override Task OnInitialize()
        {
            Teachers= await _service.SelectAllRecordAsync<Teacher>();
        }
    }
}
