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

        private int _selectedItemIndex;
        public int SelectedItemIndex 
        {
            get => _selectedItemIndex;
            set
            {
                SetValue(ref _selectedItemIndex, value);
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
        public RelayCommand SaveAndRefreshCommand { get; set; }
        public RelayCommand RemoveAllCommand { get; set; }

        public ListViewModelBase()
        {
            RemoveCommand = new RelayCommand(parameter => Remove(parameter));
            SaveAndRefreshCommand = new RelayCommand(parameter => SaveAndRefresh(parameter));
            SelectFirstRow();
        }

        public void Remove(object parameter)
        {
            if (parameter is TEntity entity) 
            {
                Delete(entity);
                if (HasItems)
                    SelectFirstRow();
                else
                    DisplaydItem = new TEntity();
            }
        }

        public void Add(object parameter)
        {     
            
        }

        public void SaveAndRefresh(object parameter)
        {
            if (parameter is TEntity entity)
            {
                Update(entity);
                SelectRowContains(entity);
            }
        }

        public void RemoveAll(object parameter)
        {
        }

        private void SelectFirstRow()
        {
            if (Items.Count > 0)
                SelectedItemIndex = 0;
        }

        private void SelectRowContains(TEntity entity)
        {
            if (entity is not object)
            {
                SelectFirstRow();
                return;
            }
            int index = GetIndex(entity);
            if (index < 0)
                SelectFirstRow();
            else
            {
                SelectedItemIndex = index;
            }
        }
    }
}
