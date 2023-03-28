using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using KretaCommandLine.Model.Abstract;
using KretaDesktop.Services;

namespace KretaDesktop.ViewModel.BaseClass
{
    public class ListViewModelBase<TEntity> : PagedListViewModelBase<TEntity>, IListViewModelBase<TEntity>
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
                    DisplaydItem = (TEntity)_selectedItem.Clone();
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

        private long _selectedItemId = -1;
        public long SelectedItemId 
        {
            get
            {
                if (DisplaydItem.Id > 0)
                    return DisplaydItem.Id;
                else if (_selectedItemId > 0)
                    return _selectedItemId;
                else
                    return -1;
            }
        }

        private bool _isNewMode = false;
        public bool IsNewMode {
            get => _isNewMode;
            set 
            {
                SetValue(ref _isNewMode, value);
                OnPropertyChanged(nameof(IsIdVisible));
                OnPropertyChanged(nameof(IsTableVisible));
                OnPropertyChanged(nameof(IsPageableVisible));
                OnPropertyChanged(nameof(IsHeaderVisible));
            }
        }

        public bool IsIdVisible => !_isNewMode;
        public bool IsTableVisible => !_isNewMode;
        public bool IsPageableVisible => !_isNewMode; 
        public bool IsHeaderVisible => !_isNewMode;

        public RelayCommand NewCommand { get; set; }
        public RelayCommand RemoveCommand { get; set; }
        public RelayCommand SaveAndRefreshCommand { get; set; }
        public RelayCommand ClearFormCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand RemoveAllCommand { get; set; }

        public ListViewModelBase(IAPIService service) : base(service)
        {
            RemoveCommand = new RelayCommand(parameter => Remove(parameter));
            SaveAndRefreshCommand = new RelayCommand(parameter => SaveAndRefresh(parameter));
            NewCommand = new RelayCommand(execute => New());
            CancelCommand = new RelayCommand(execute => Cancel());
            ClearFormCommand = new RelayCommand(execute => Clear());
            SelectFirstRow();
        }

        public async void Remove(object parameter)
        {
            if (parameter is TEntity entity) 
            {
                await Delete(entity);
                if (HasItems)
                    SelectFirstRow();
                else
                    DisplaydItem = new TEntity();
            }
        }

        public void New()
        {
            _selectedItemId=DisplaydItem.Id;
            DisplaydItem = new TEntity();
            IsNewMode = true;
        }

        public async void SaveAndRefresh(object parameter)
        {
            if (parameter is TEntity entity)
            {
                if (entity.HasId)
                {
                    // Mivel van id-je akkor menthet
                    Update(entity);                    
                }
                else
                { 
                    // Új adat felvitel
                    await Insert(entity);
                    IsNewMode = false;
                }
                SelectRowContains(entity);
            }            
        }

        public void Cancel()
        {
            IsNewMode = false;
            SelectedItemIndex = -1;
            SelectRowContains(SelectedItemId);            
        }

        public void Clear()
        {
            DisplaydItem = new TEntity();
        }

        public void RemoveAll(object parameter)
        {
        }

        protected void InitializePage()
        {
            InitializePagedPage();
        }

        private void SelectFirstRow()
        {
            if (Items.Count > 0)
                SelectedItemIndex = 0;
        }

        private void SelectRowContains(TEntity entity)
        {
            if (entity is not object)
                SelectFirstRow();
            else
            {
                int index = GetIndex(entity);
                if (index < 0)
                    SelectFirstRow();
                else
                    SelectedItemIndex = index;
            }
        }

        private void SelectRowContains(long id)
        {
            if (id <= 0)
                SelectFirstRow();
            else
            {
                int index = GetIndex(id);
                if (index < 0)
                    SelectFirstRow();
                else                 
                    SelectedItemIndex = index;
            }
        }


    }
}
