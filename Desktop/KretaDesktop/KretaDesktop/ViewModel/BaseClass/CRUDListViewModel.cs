using KretaCommandLine.Model.Abstract;
using KretaDesktop.Services;
using KretaDesktop.ViewModel.BaseClass.Interface;
using KretaDesktop.ViewModel.Command;
using System;
using System.Threading.Tasks;

namespace KretaDesktop.ViewModel.BaseClass
{
    public class CRUDListViewModel<TEntity> : ListViewModel<TEntity>, ICRUDListViewModelBase<TEntity>
        where TEntity : ClassWithId, new()
    {
        private string _itemFilter;
        public string ItemFilter
        {
            get => _itemFilter;
            set => SetValue(ref _itemFilter, value);
        }

        public CRUDListViewModel(IAPIService service) : base(service)
        {
            RemoveCommand = new AsyncRelayCommandWithParameter(parameter => Remove(parameter), (ex) => OnException());
            SaveAndRefreshCommand = new AsyncRelayCommandWithParameter(parameter => SaveAndRefresh(parameter), (ex) => OnException());
            NewCommand = new RelayCommand(execute => New());
            CancelCommand = new RelayCommand(execute => Cancel());
            ClearFormCommand = new RelayCommand(execute => Clear());
            FilterItemsCommand = new AsyncRelayCommand(OnFilterItems, (ex) => OnException());
            IsCRUDVisible = true;
        }

        public RelayCommand NewCommand { get; set; }
        public AsyncRelayCommandWithParameter RemoveCommand { get; set; }
        public AsyncRelayCommandWithParameter SaveAndRefreshCommand { get; set; }
        public RelayCommand ClearFormCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand RemoveAllCommand { get; set; }
        public AsyncRelayCommand FilterItemsCommand { get; set; }

        protected async Task OnFilterItems()
        {
            await FilterItems(_itemFilter);
        }

        protected async Task Remove(object parameter)
        {
            if (parameter is TEntity entity)
            {
                await DeleteRecord(entity);
                if (HasItems)
                    SelectFirstRow();
                else
                    DisplayedItem = new TEntity();
            }
        }

        protected async Task SaveAndRefresh(object parameter)
        {
            if (parameter is TEntity entity)
            {
                await SaveRecord(entity);
                IsNewMode = false;
                SelectRowContains(entity);
            }
        }

        protected void New()
        {            
            DisplayedItem = new TEntity();
            IsNewMode = true;
        }

        protected void Cancel()
        {
            IsNewMode = false;
            SelectedItemIndex = -1;
            SelectRowContains(SelectedItemId);
        }

        protected void Clear()
        {
            DisplayedItem = new TEntity();
        }

        private void OnException()
        {
        }

    }
}
