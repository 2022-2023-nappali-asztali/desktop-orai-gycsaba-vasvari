using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KretaCommandLine.Model;
using KretaDesktop.ViewModel.BaseClass;

namespace KretaDesktop.ViewModel.Content
{
    public class ListSubjectViewModel : ListViewModelBase<Subject>
    {
        public ListSubjectViewModel()
        {
            // Backendről jön
            List<Subject> subjects = new List<Subject>();
            subjects.Add(new Subject(1, "Matematika"));
            subjects.Add(new Subject(2, "Magyar nyelv"));
            subjects.Add(new Subject(3, "Történelem"));

            // Ősosztály
            Insert(subjects);
        }
    }
}
