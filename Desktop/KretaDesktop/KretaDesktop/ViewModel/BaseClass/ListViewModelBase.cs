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
        where TEntity : class, new()
    {
        private TEntity selectedItem;
        public TEntity SelectedItem 
        { 
            get => selectedItem; 
            set 
            {
                SetValue(ref selectedItem, value);
            }
        }

        private TEntity displaydItem = new();
        public TEntity DisplaydItem
        {
            get => displaydItem;
            set
            {
                SetValue(ref displaydItem, value);
            }
        } 

        public RelayCommand DeleteCommand { get; set; }

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
