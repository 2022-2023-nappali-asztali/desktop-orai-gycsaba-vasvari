using KretaDesktop.ViewModel.BaseClass;
using System.Threading.Tasks;

namespace KretaDesktop.ViewModel.Content
{
    public class StudentPerClassViewModel : InitializedViewModelBase
    {
        public ObservableCollection<Student>

        public override Task OnInitialize()
        {
            return Task.CompletedTask;
        }
    }
}
