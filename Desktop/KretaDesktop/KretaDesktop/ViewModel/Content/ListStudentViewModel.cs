using KretaCommandLine.Model;
using KretaDesktop.Services;
using KretaDesktop.ViewModel.BaseClass;
using System.Threading.Tasks;

namespace KretaDesktop.ViewModel.Content
{
    public class ListStudentViewModel : PagedCRUDListViewModel<Student>
    {
        public ListStudentViewModel(IAPIService service) : base(service)
        {
        }

        public override async Task OnInitialize()
        {
            await InitializePageWithIncludedData();
        }

        public async Task OnInitialize(SchoolClass schoolClass)
        {
            
        }
    }
}
