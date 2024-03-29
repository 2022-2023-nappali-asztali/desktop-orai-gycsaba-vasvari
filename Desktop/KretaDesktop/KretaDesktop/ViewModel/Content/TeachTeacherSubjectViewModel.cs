﻿using KretaCommandLine.Comparer;
using KretaCommandLine.Model;
using KretaDesktop.Services;
using KretaDesktop.ViewModel.BaseClass;
using KretaDesktop.ViewModel.Command;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace KretaDesktop.ViewModel.Content
{
    public class TeachTeacherSubjectViewModel : ServiceViewModelBase<Teacher>
    {
        private IAPIService _service;
        private IWrapService _wrapService;

        private List<Subject> _allSubjects = new List<Subject>();

        private ObservableCollection<Teacher> _teachers;
        public ObservableCollection<Teacher> Teachers
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

        private ObservableCollection<Subject> _subjectOfTeacher;
        public ObservableCollection<Subject> SubjectsOfTeacher
        {
            get { return _subjectOfTeacher; }
            set { SetValue(ref _subjectOfTeacher, value); }
        }

        private ObservableCollection<Subject> _otherSubjects;
        public ObservableCollection<Subject> OtherSubjects
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

        private Subject _selectedTeacherToDelete;
        public Subject SelectedSubjectToDelete
        {
            get { return _selectedTeacherToDelete; }
            set { SetValue(ref _selectedTeacherToDelete, value); }
        }



        public TeachTeacherSubjectViewModel(IAPIService service, IWrapService wrapService) : base(service)
        {
            _service = service;
            _wrapService = wrapService;

            AddSubjectCommand = new AsyncRelayCommand(OnAddSubject, (ex) => OnException());
            DeleteSubjectCommand = new AsyncRelayCommand(OnDeleteSubject, (ex) => OnException());
        }

        public AsyncRelayCommand AddSubjectCommand { get; private set; }
        public AsyncRelayCommand DeleteSubjectCommand { get; private set; }

        public async override Task OnInitialize()
        {
            _allSubjects = await _service.SelectAllRecordAsync<Subject>();
            List<Teacher> teachers= await _service.SelectAllRecordAsync<Teacher>();
            Teachers = new ObservableCollection<Teacher>(teachers);
        }

        private async void Refresh()
        {
            if (_selectedTeacher is object)
            {
                List<Subject> allSubjects = await _wrapService.GetTeacherSubjects(_selectedTeacher.Id);
                SubjectsOfTeacher = new ObservableCollection<Subject>(allSubjects);
                SubjectComparer comparer = new SubjectComparer();
                List<Subject> otherSubject= _allSubjects.Except(SubjectsOfTeacher, comparer).ToList();
                OtherSubjects = new ObservableCollection<Subject>(otherSubject);
            }
        }

        private async Task OnAddSubject()
        {
            if (SelectedTeacher is object && SelectedNoTeachedSubject is object)
            {
                TeachTeacherSubject newTeachTeacherSubject = new TeachTeacherSubject(SelectedTeacher.Id, SelectedNoTeachedSubject.Id);
                await _service.SaveNewEntity<TeachTeacherSubject>(newTeachTeacherSubject);
                Refresh();
            }
        }

        private async Task OnDeleteSubject()
        {
            if (SelectedTeacher is object && SelectedSubjectToDelete is object)
            {
                TeachTeacherSubject teachTeachserSubjectToDelete = new TeachTeacherSubject(SelectedTeacher.Id, SelectedSubjectToDelete.Id);
                await _service.DeleteObject(teachTeachserSubjectToDelete);
                Refresh();
            }
        }

        private void OnException()
        {

        }
    }
}
