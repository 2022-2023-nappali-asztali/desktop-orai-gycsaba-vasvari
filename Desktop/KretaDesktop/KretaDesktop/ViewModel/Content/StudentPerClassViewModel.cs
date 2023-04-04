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
        public ObservableCollection<SchoolClass> SchoolClasses;
        

        public StudentPerClassViewModel(IAPIService service) : base(service)
        {
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
