using System.Collections.ObjectModel;
using KretaCommandLine.Model.Abstract;
using KretaDesktop.Services;
using KretaDesktop.ViewModel.BaseClass.Interface;
using KretaDesktop.ViewModel.Command;

namespace KretaDesktop.ViewModel.BaseClass
{
    public class ListViewModelBase<TEntity> : ViewModelBase<TEntity, ObservableCollection<TEntity>>, IListViewModelBase<TEntity>
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
                    DisplaydItem = (TEntity) _selectedItem.Clone();
                }
            }
        }

        private int _selectedItemIndex;
        public int SelectedItemIndex
        {
            get => _selectedItemIndex;
            set => SetValue(ref _selectedItemIndex, value);           
        }


        private TEntity _displaydItem = new();
        public TEntity DisplaydItem
        {
            get => _displaydItem;
            set => SetValue(ref _displaydItem, value);
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

        private bool isPageableVisible;
        public bool IsPageableVisible
        {
            get => isPageableVisible && !_isNewMode;
            set => SetValue(ref isPageableVisible, value);
        }
            

        public bool IsIdVisible => !_isNewMode;
        public bool IsTableVisible => !_isNewMode;        
        public bool IsHeaderVisible => !_isNewMode;
      
        public ListViewModelBase(IAPIService service) : base(service)
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
    }
}
