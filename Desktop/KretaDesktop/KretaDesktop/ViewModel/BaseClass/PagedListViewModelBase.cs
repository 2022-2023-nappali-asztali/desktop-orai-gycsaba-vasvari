using KretaCommandLine.Model.Abstract;
using KretaDesktop.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KretaDesktop.ViewModel.BaseClass
{
    public abstract class PagedListViewModelBase<TEntity> : ViewModelBase<TEntity, List<TEntity>>, IPagedListViewModelBase<TEntity>
        where TEntity : ClassWithId, new()
    {
        public PagedListViewModelBase(IAPIService service) : base(service)
        {
            FirstPageCommand = new RelayCommand(execute => GoToFirstPage());
            PreviousPageCommand = new RelayCommand(execute => GoToPreviousPage());
            NextPageCommand = new RelayCommand(execute => GoToNextPage());
            LastPageCommand = new RelayCommand(execute => GoToLastPage());
        }

        public RelayCommand FirstPageCommand { get; private set; }
        public RelayCommand PreviousPageCommand { get; private set; }
        public RelayCommand NextPageCommand { get; private set; }
        public RelayCommand LastPageCommand { get; private set; }

        
        protected async Task GetPageAsync()
        {
            if (QueryStringParameters.IsCorrect)
            {
                PagedList = await _service.GetPageAsync<TEntity>(QueryStringParameters);
                QueryStringParameters.Populate(PagedList);
            }
            else 
            {
                PagedList = new PagedList<TEntity>();
            }
        }

        protected void SetPagedList()
        {
            
        }

        protected virtual void RefreshPagedItems()
        {}

        private async void GoToFirstPage() 
        {
            if (QueryStringParameters.CurrentPage != 0)
            {
                QueryStringParameters.CurrentPage = 0;
                await GetPageAsync();
                RefreshPagedItems();
            }
        }
        private async void GoToPreviousPage() 
        { 
            if (QueryStringParameters.HasPreviousPage)
            {
                QueryStringParameters.CurrentPage--;
                await GetPageAsync();
                RefreshPagedItems();
            }
        }
        private async void GoToNextPage() 
        {
            if (QueryStringParameters.HasNextPage)
            {
                QueryStringParameters.CurrentPage++;
                await GetPageAsync();
                RefreshPagedItems();
            }
        }
        private async void GoToLastPage() 
        {
            if (QueryStringParameters.CurrentPage != QueryStringParameters.NumberOfPage - 1)
            {
                QueryStringParameters.CurrentPage = QueryStringParameters.NumberOfPage - 1;
                await GetPageAsync();
                RefreshPagedItems();
            }
        }
    }
}
