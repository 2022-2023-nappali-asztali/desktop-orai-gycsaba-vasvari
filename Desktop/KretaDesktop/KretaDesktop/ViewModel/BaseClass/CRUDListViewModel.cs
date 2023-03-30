using KretaCommandLine.Model.Abstract;
using KretaDesktop.Services;
using KretaDesktop.ViewModel.BaseClass.Interface;
using KretaDesktop.ViewModel.Command;

namespace KretaDesktop.ViewModel.BaseClass
{
    public class CRUDListViewModel<TEntity> : ListViewModel<TEntity>, ICRUDListViewModelBase<TEntity>
        where TEntity : ClassWithId, new()
    {

        public CRUDListViewModel(IAPIService service) : base(service)
        {
            RemoveCommand = new RelayCommand(parameter => Remove(parameter));
            SaveAndRefreshCommand = new RelayCommand(parameter => SaveAndRefresh(parameter));
            NewCommand = new RelayCommand(execute => New());
            CancelCommand = new RelayCommand(execute => Cancel());
            ClearFormCommand = new RelayCommand(execute => Clear());
        }

        public RelayCommand NewCommand { get; set; }
        public RelayCommand RemoveCommand { get; set; }
        public RelayCommand SaveAndRefreshCommand { get; set; }
        public RelayCommand ClearFormCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand RemoveAllCommand { get; set; }

        protected async void Remove(object parameter)
        {
            if (parameter is TEntity entity)
            {
                await DeleteRecord(entity);
                if (HasItems)
                    SelectFirstRow();
                else
                    DisplaydItem = new TEntity();
            }
        }

        protected async void SaveAndRefresh(object parameter)
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
            DisplaydItem = new TEntity();
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
            DisplaydItem = new TEntity();
        }

    }
}
