using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using KretaCommandLine.Model;

namespace KretaDesktop.ViewModel.BaseClass
{
    public class ViewModelBase<TEntity, TCollection> 
        where TEntity : class
        where TCollection : ICollection<TEntity>
    {

        private TCollection collection;

        public void Add(TEntity entiy)
        {
            collection.Add(entiy);
        }
    }

    public class SubjectViewModel : ViewModelBase<Subject,ObservableCollection<Subject>>
    {
        Subject subject = new Subject(1, "Töri");

        public SubjectViewModel()
        {
            Add(subject);
        }
    }

    public class StudentViewModel : ViewModelBase<Student, ObservableCollection<Student>>
    {
        Student student = new Student();

        public StudentViewModel()
        {
            Add(student);
        }

    }
        



}
