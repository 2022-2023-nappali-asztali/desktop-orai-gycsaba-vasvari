using APIHelpersLibrary.Paged;
using KretaCommandLine.Model.Abstract;
using KretaDesktop.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KretaDesktop.ViewModel.BaseClass
{
    public abstract class PagedViewModelBase<TEntity> : ViewModelBase<TEntity, List<TEntity>>, IPagedListViewModelBase<TEntity>
        where TEntity : ClassWithId, new()
    {
        private ItemParameters _itemParameters= new ItemParameters(5);
        private MetaData _metaData = new MetaData();

        public PagedViewModelBase(IAPIService service) : base(service)
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


        protected async override Task InitializePage()
        {
            await RefreshItems();
        }

        protected async override Task RefreshItems()
        {
            PagingResponse<TEntity> result = await _service.GetPageAsync<TEntity>(_itemParameters);
            _metaData = result.MetaData;
            Items = result.Items;            
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
