using KretaCommandLine.Model;
using KretaDesktop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KretaDesktop.ViewModel.Content
{
    public class ListStudentByClassViewModel : ListStudentViewModel, IListStudentByClassViewModel
    {
        private SchoolClass _selectedSchoolClass;

        public ListStudentByClassViewModel(IAPIService service) : base(service)
        {
        }

        public async Task OnInitialize(SchoolClass schoolClass)
        {
            _selectedSchoolClass=schoolClass;
            await RefreshItems();

        }

        protected override async Task RefreshItems()
        {
            IStudentAPIService studentAPIService = new StudentAPIService();
            List<Student> students = await studentAPIService.SelectStudentOfClass<Student>(_selectedSchoolClass.Id);
            DeleteAllItems();
            AddToItems(students);
        }
    }
}
