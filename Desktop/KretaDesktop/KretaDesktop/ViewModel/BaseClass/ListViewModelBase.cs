using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;

namespace KretaDesktop.ViewModel.BaseClass
{
    public class ListViewModelBase<TEntity> : ViewModelBase<TEntity,ObservableCollection<TEntity>> 
        where TEntity : class
    {
        public TEntity SelectedItem { get; set; }

        RelayCommand DeleteCommand { get; set; }

        public ListViewModelBase()
        {
            DeleteCommand = new RelayCommand(parameter => Delete(parameter));
        }

        public void Delete(object parameter)
        {
            if (parameter is TEntity entity) 
            {
                Remove(entity);
            }
        }
    }
}
