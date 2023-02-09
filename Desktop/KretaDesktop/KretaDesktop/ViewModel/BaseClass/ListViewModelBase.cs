using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using KretaCommandLine.Model.Abstract;

namespace KretaDesktop.ViewModel.BaseClass
{
    public class ListViewModelBase<TEntity> : ViewModelBase<TEntity,ObservableCollection<TEntity>>, IListViewModelBase<TEntity>
        where TEntity : ClassWithId, new()
    {
        private TEntity _selectedItem;
        public TEntity SelectedItem 
        { 
            get => _selectedItem; 
            set 
            {
                SetValue(ref _selectedItem, value);
                if (_selectedItem is object) // is object
                {
                    DisplaydItem = (TEntity) _selectedItem.Clone();
                }
            }
        }

        private TEntity _displaydItem = new();
        public TEntity DisplaydItem
        {
            get => _displaydItem;
            set
            {
                SetValue(ref _displaydItem, value);
            }
        } 
        
        public RelayCommand AddCommand { get; set; }
        public RelayCommand RemoveCommand { get; set; }
        public RelayCommand RefreshCommand { get; set; }
        public RelayCommand RemoveAllCommand { get; set; }

        public ListViewModelBase()
        {
            RemoveCommand = new RelayCommand(parameter => Remove(parameter));
        }

        public void Remove(object parameter)
        {
            if (parameter is TEntity entity) 
            {
                Delete(entity);
            }
        }

        public void Add(object parameter)
        {     
            
        }

        public void Refresh(object parameter)
        {
        }

        public void RemoveAll(object parameter)
        {
        }
    }
}
