using KretaCommandLine.Model;
using KretaCommandLine.QueryParameter;
using KretaDesktop.Services;
using KretaDesktop.ViewModel.BaseClass;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace KretaDesktop.ViewModel.Content
{
    public class ListStudentViewModel : CRUDListViewModel<Student>
    {
        private ObservableCollection<Student> _studentsHaveNoClass;
        public ObservableCollection<Student> StudentsHaveNoClass
        {
            get { return _studentsHaveNoClass; }
            set { SetValue(ref _studentsHaveNoClass, value); }
        }

        public ListStudentViewModel(IAPIService service) : base(service)
        {
            SearchedPropertyName = nameof(Student.Name);

            List<SortingData> sortingDatas=new List<SortingData>();
            sortingDatas.Add(new SortingData("Rendezés", "", ""));
            sortingDatas.Add(new SortingData("Név - növekvő", "Diák neve szerint növekvő sorrend", "name asc"));
            sortingDatas.Add(new SortingData("Név - csökkenő", "Diák neve szerint csökkenő sorrend", "name desc"));
            sortingDatas.Add(new SortingData("Osztály - növekvő", "Tantárgy neve szerint növekvő sorrend", "SchoolClassOfStudent asc"));
            sortingDatas.Add(new SortingData("Osztály - csökkenő", "Tantárgy neve szerint csökkenő sorrend", "SchoolClassOfStudent desc"));

            SortingDatas =sortingDatas;
            _studentsHaveNoClass= new ObservableCollection<Student>();
        }



        public override async Task OnInitialize()
        {
            await InitializeWithIncludedData();

            await RefreshStudentNoClass();
        }

        protected override async Task RefreshItems()
        {
            await base.RefreshItems();
            await RefreshStudentNoClass();
        }

        private async Task RefreshStudentNoClass()
        {
            _studentsHaveNoClass.Clear();
            List<Student> noClass = await _service.SelectAllByIdProperty<Student>("SchoolClassId", -1);
            StudentsHaveNoClass = new ObservableCollection<Student>(noClass);
        }
    }
}
