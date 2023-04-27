using KretaCommandLine.Comparer;
using KretaCommandLine.Model;
using KretaDesktop.Services;
using KretaDesktop.ViewModel.BaseClass;
using KretaDesktop.ViewModel.Command;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace KretaDesktop.ViewModel.Content
{
    public class TeachTeacherSubjectViewModel : ServiceViewModelBase<Teacher>
    {
        private IAPIService _service;
        private IWrapService _wrapService;

        private List<Subject> _allSubjects= new List<Subject>();

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

        private List<Subject> _otherSubjects;
        public List<Subject> OtherSubjects
        {
            get { return _otherSubjects; }
            set { SetValue(ref _otherSubjects, value); }
        }

        private Subject _selectedNoTeachedSubject;
        public Subject SelectedNoTeachedSubject
        {
            get { return _selectedNoTeachedSubject; }
            set { SetValue(ref _selectedNoTeachedSubject, value); }
        }


        public TeachTeacherSubjectViewModel(IAPIService service, IWrapService wrapService) : base(service)
        {
            _service = service;
            _wrapService = wrapService;

            AddSubjectCommand = new AsyncRelayCommand(OnAddSubject, (ex) => OnException());
        }

        public AsyncRelayCommand AddSubjectCommand { get; private set; }

        public async override Task OnInitialize()
        {
            _allSubjects = await _service.SelectAllRecordAsync<Subject>();
            Teachers = await _service.SelectAllRecordAsync<Teacher>();
        }

        private async void Refresh()
        {
            if (_selectedTeacher is object)
            {                
                SubjectsOfTeacher = await _wrapService.GetTeacherSubjects(_selectedTeacher.Id);
                SubjectComparer comparer = new SubjectComparer();
                OtherSubjects = _allSubjects.Except(SubjectsOfTeacher,comparer).ToList();
            }
        }

        private async Task OnAddSubject()
        {
            TeachTeacherSubject newTeachTeacherSubject = new TeachTeacherSubject(SelectedTeacher.Id, SelectedNoTeachedSubject.Id);
            await _service.SaveNewEntity<TeachTeacherSubject>(newTeachTeacherSubject);
            Refresh();

        }

        private void OnException()
        {

        }
    }
}
