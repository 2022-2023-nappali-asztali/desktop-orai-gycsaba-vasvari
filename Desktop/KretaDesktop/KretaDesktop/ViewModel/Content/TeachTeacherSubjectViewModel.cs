using KretaCommandLine.Model;
using KretaDesktop.Services;
using KretaDesktop.ViewModel.BaseClass;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace KretaDesktop.ViewModel.Content
{
    public class TeachTeacherSubjectViewModel : ServiceViewModelBase<Teacher>
    {
        private IAPIService _service;
        private IWrapService _wrapService;

        private List<Teacher> _teachers;
        public List<Teacher> Teachers
        {
            get { return _teachers; }
            set { SetValue(ref _teachers, value); }
        }

        private Teacher _selectedTeacher;
        public Teacher SelectedTeacher
        {
            get { return _selectedTeacher; }
            set 
            { 
                SetValue(ref _selectedTeacher, value);
                Refresh();
            }
        }

        private List<Subject> _subjectOfTeacher;
        public  List<Subject> SubjectsOfTeacher
        {
            get { return _subjectOfTeacher; }
            set { SetValue(ref _subjectOfTeacher, value); }
        }

        public TeachTeacherSubjectViewModel(IAPIService service, IWrapService wrapService) : base(service)
        {
            _service = service;
            _wrapService = wrapService;
        }

        public async override Task OnInitialize()
        {
            Teachers = await _service.SelectAllRecordAsync<Teacher>();
        }

        private async void Refresh()
        {
            if (_selectedTeacher is object)
            {
                SubjectsOfTeacher = await _wrapService.GetTeacherSubjects(_selectedTeacher.Id);
            }
        }
    }
}
