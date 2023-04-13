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
            SearchedPropertyName = nameof(Student.Name);
        }

        public override async Task OnInitialize()
        {
            await InitializeWithIncludedData();
        }
    }
}
