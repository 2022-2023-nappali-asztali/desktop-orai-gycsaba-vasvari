﻿using System.Collections.ObjectModel;
using System.Threading.Tasks;
using KretaCommandLine.Model.Abstract;
using KretaDesktop.Services;
using KretaDesktop.ViewModel.BaseClass.Interface;
using KretaDesktop.ViewModel.Command;

namespace KretaDesktop.ViewModel.BaseClass
{
    public class ListViewModel<TEntity> : ViewModelBase<TEntity, ObservableCollection<TEntity>>, IListViewModelBase<TEntity>
        where TEntity : ClassWithId, new()
    {
        private TEntity _selectedItem;
        public TEntity SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetValue(ref _selectedItem, value);
                if (_selectedItem is object) 
                {
                    DisplayedItem = (TEntity) _selectedItem.Clone();
                }
            }
        }

        private int _selectedItemIndex;
        public int SelectedItemIndex
        {
            get => _selectedItemIndex;
            set => SetValue(ref _selectedItemIndex, value);           
        }


        private TEntity _displayedItem = new();
        public TEntity DisplayedItem
        {
            get => _displayedItem;
            set => SetValue(ref _displayedItem, value);
        }

        private long _selectedItemId = -1;
        public long SelectedItemId 
        {
            get
            {
                if (DisplayedItem.Id > 0)
                    return DisplayedItem.Id;
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

        private bool _isPageableVisible;
        public bool IsPageableVisible
        {
            get => _isPageableVisible && !_isNewMode;
            set => SetValue(ref _isPageableVisible, value);
        }

        private bool _isCRUDVisible=false;
        public bool IsCRUDVisible
        {
            get => _isCRUDVisible;
            set => SetValue(ref _isCRUDVisible, value);
        }

        private bool _isSearchAndSortVisible = true;
        public bool IsSearchAndSortVisible
        {
            get => _isSearchAndSortVisible;
            set => SetValue(ref _isSearchAndSortVisible, value);
        }

        public bool IsIdVisible => !_isNewMode;
        public bool IsTableVisible => !_isNewMode;        
        public bool IsHeaderVisible => !_isNewMode;
      
        public ListViewModel(IAPIService service) : base(service)
        {         
            IsPageableVisible = false;
            SelectFirstRow();
        }

        protected void SelectFirstRow()
        {
            if (Items.Count > 0)
                SelectedItemIndex = 0;
        }

        protected void SelectRowContains(TEntity entity)
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

        protected void SelectRowContains(long id)
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

        public override Task OnInitialize()
        {
            return Task.CompletedTask;
        }
    }
}
