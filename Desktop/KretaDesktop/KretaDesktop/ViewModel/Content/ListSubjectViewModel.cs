using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KretaCommandLine.Model;
using KretaDesktop.Services;
using KretaDesktop.ViewModel.BaseClass;

namespace KretaDesktop.ViewModel.Content
{
    public class ListSubjectViewModel : ListViewModelBase<Subject>
    {
        public ListSubjectViewModel(APIService service) : base(service)
        {
            InitializePage();
        }
    }
}
