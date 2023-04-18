using KretaCommandLine.Model;
using KretaCommandLine.QueryParameter;
using KretaDesktop.Services;
using KretaDesktop.ViewModel.BaseClass;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KretaDesktop.ViewModel.Content
{
    public class ListSubjectViewModel : PagedCRUDListViewModel<Subject>
    {
        public ListSubjectViewModel(IAPIService service) : base(service)
        {
            SearchedPropertyName = nameof(Subject.SubjectName);

            List<SortingData> sortingDatas = new List<SortingData>();
            sortingDatas.Add(new SortingData("Rendezés", "", ""));
            sortingDatas.Add(new SortingData("Név - növekvő", "Tantárgy neve szerint növekvő sorrend", "subjectname asc"));
            sortingDatas.Add(new SortingData("Név - csökkenő", "Tantárgy neve szerint csökkenő sorrend", "subjectname desc"));
            SortingDatas=sortingDatas;
        }

        public override async Task OnInitialize()
        {
            await InitializeItems();
        }
    }
}
