using KretaCommandLine.Model;
using KretaDesktop.Services;
using KretaDesktop.ViewModel.BaseClass;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace KretaDesktop.ViewModel.Content
{
    public class StudentPerClassViewModel : ServiceViewModelBase<SchoolClass>
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
            set => SetValue(ref _selectedSchoolClass, value);
        }

        public StudentPerClassViewModel(IAPIService service) : base(service)
        {
            SchoolClasses = new ObservableCollection<SchoolClass>();
        }

        public override async Task OnInitialize()
        {
            SchoolClasses.Clear();
            List<SchoolClass> schoolClasses = await _service.SelectAllRecordAsync<SchoolClass>();
            SchoolClasses = new ObservableCollection<SchoolClass>(schoolClasses);
            return; 
        }
    }
}
