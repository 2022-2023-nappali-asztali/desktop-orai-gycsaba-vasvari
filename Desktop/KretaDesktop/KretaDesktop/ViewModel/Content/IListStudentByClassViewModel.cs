using KretaCommandLine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KretaDesktop.ViewModel.Content
{
    public interface IListStudentByClassViewModel
    {
        public Task OnInitialize(SchoolClass schoolClass);
    }
}
