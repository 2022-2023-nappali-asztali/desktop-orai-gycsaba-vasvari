using KretaCommandLine.Model;
using KretaDesktop.Services;
using KretaDesktop.ViewModel.BaseClass;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KretaDesktop.ViewModel.Content
{
    public class StudentOfClassViewModel : ServiceViewModelBase<SchoolClass>
    {
        private IAPIService _service;
        private IWrapService _wrapService;

        private List<SchoolClass> _schoolClasses;
        public List<SchoolClass> SchoolClasses
        {
            get { return _schoolClasses; }
            set { SetValue(ref _schoolClasses, value); }
        }

        public StudentOfClassViewModel(IAPIService service, IWrapService wrapService) : base(service)
        {
            _service = service;
            _wrapService = wrapService;
        }

        public async override Task OnInitialize()
        {
            SchoolClasses= await _service.SelectAllRecordAsync<SchoolClass>();
        }
    }
}
