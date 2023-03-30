using APIHelpersLibrary.Paged;
using KretaCommandLine.Model.Abstract;
using KretaDesktop.Services;
using KretaDesktop.ViewModel.BaseClass.Interface;
using KretaDesktop.ViewModel.Command;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace KretaDesktop.ViewModel.BaseClass
{
    public abstract class PagedViewModel<TEntity> : ViewModelBase<TEntity, ObservableCollection<TEntity>>, IPagedListViewModelBase<TEntity>
        where TEntity : ClassWithId, new()
    {
        private ItemParameters _itemParameters = new ItemParameters(5);
        private MetaData _metaData = new MetaData();
        public MetaData MetaData
        {
            get { return _metaData; }
            set 
            { 
                SetValue(ref _metaData, value);
                OnPropertyChanged(nameof(PageInformation));
            }
        }

        public PagedViewModel(IAPIService service) : base(service)
        {
            FirstPageCommand = new AsyncRelayCommand(GoToFirstPage, (ex) => OnException());
            PreviousPageCommand = new AsyncRelayCommand(GoToPreviousPage, (ex) => OnException());
            NextPageCommand = new AsyncRelayCommand(GoToNextPage, (ex) => OnException());
            LastPageCommand = new AsyncRelayCommand(GoToLastPage, (ex) => OnException());
        }        

        public AsyncRelayCommand FirstPageCommand { get; private set; }
        public AsyncRelayCommand PreviousPageCommand { get; private set; }
        public AsyncRelayCommand NextPageCommand { get; private set; }
        public AsyncRelayCommand LastPageCommand { get; private set; }

        public string PageInformation => $"Oldal: {MetaData.CurrentPage} / {MetaData.TotalPages}";

        protected async override Task InitializePage()
        {
            await RefreshItems();
        }

        protected async override Task RefreshItems()
        {
            PagingResponse<TEntity> result = await _service.GetPageAsync<TEntity>(_itemParameters);
            MetaData = result.MetaData;
            DeleteAllItems();
            AddToItems(result.Items);            
        }


        private async Task GoToFirstPage() 
        {
            _itemParameters.PageNumber = 1;
            await RefreshItems();            
        }

        private  async Task GoToLastPage()
        {
            _itemParameters.PageNumber = _metaData.TotalPages;
            await RefreshItems();
        }

        private async Task GoToPreviousPage() 
        { 
            if (_metaData.HasPrevious)
            {
                _itemParameters.PageNumber = _metaData.CurrentPage - 1;
                await RefreshItems();
            }
        }

        private async Task GoToNextPage() 
        {
            if (_metaData.HasNext)
            {
                _itemParameters.PageNumber=_metaData.CurrentPage + 1;
                await RefreshItems();
            }
        }        

        private void OnException()
        {

        }
    }
}
