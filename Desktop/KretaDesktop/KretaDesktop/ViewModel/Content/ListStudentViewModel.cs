using KretaCommandLine.Model;
using KretaDesktop.Services;
using KretaDesktop.ViewModel.BaseClass;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KretaDesktop.ViewModel.Content
{
    public class ListStudentViewModel : CRUDListViewModel<Student>
    {
        public ListStudentViewModel(IAPIService service) : base(service)
        {
        }

        public override async Task OnInitialize()
        {
            await InitializeWithIncludedData();
        }

        public async Task OnInitialize(SchoolClass schoolClass)
        {
            IStudentAPIService studentAPIService = new StudentAPIService();
            List<Student> students = await studentAPIService.SelectStudentOfClass<Student>(schoolClass.Id);
            //Items = _service;
            await RefreshItems();

        }

        protected override Task RefreshItems()
        {
            return base.RefreshItems();
        }
    }
}
