using KretaCommandLine.Model;
using KretaDesktop.Services;
using KretaDesktop.ViewModel.BaseClass;
using System.Threading.Tasks;

namespace KretaDesktop.ViewModel.Content
{
    public class ListSubjectViewModel : PagedViewModelBase<Subject>
    {
        public ListSubjectViewModel(IAPIService service) : base(service)
        {            
        }

        public async Task OnInitialize()
        {
            await InitializePage();
        }
    }
}
