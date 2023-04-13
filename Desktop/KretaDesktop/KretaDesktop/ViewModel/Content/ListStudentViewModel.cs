using KretaCommandLine.Model;
using KretaCommandLine.QueryParameter;
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

            List<SortingData> sortingDatas=new List<SortingData>();
            sortingDatas.Add(new SortingData("Rendezés", "", ""));
            sortingDatas.Add(new SortingData("Név - növekvő", "Diák neve szerint növekvő sorrend", "name asc"));
            sortingDatas.Add(new SortingData("Név - csökkenő", "Diák neve szerint csökkenő sorrend", "name desc"));
            sortingDatas.Add(new SortingData("Osztály - növekvő", "Tantárgy neve szerint növekvő sorrend", "displayedSchoolClass asc"));
            sortingDatas.Add(new SortingData("Osztály - csökkenő", "Tantárgy neve szerint csökkenő sorrend", "displayedSchoolClass desc"));

            SortingDatas =sortingDatas;
        }

        public override async Task OnInitialize()
        {
            await InitializeWithIncludedData();
        }
    }
}
