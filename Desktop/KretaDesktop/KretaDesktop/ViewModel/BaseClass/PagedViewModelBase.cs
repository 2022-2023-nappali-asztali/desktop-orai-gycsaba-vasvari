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
            FirstPageCommand = new RelayCommand(execute => GoToFirstPage());
            PreviousPageCommand = new RelayCommand(execute => GoToPreviousPage());
            NextPageCommand = new RelayCommand(execute => GoToNextPage());
            LastPageCommand = new RelayCommand(execute => GoToLastPage());
        }

        public RelayCommand FirstPageCommand { get; private set; }
        public RelayCommand PreviousPageCommand { get; private set; }
        public RelayCommand NextPageCommand { get; private set; }
        public RelayCommand LastPageCommand { get; private set; }


        protected override void InitializePage()
        {
            RefreshItems();
        }

        protected override async void RefreshItems()
        {
            PagingResponse<TEntity> result = await _service.GetPageAsync<TEntity>(_itemParameters);
            _metaData = result.MetaData;
            Items = result.Items;            
        }


        private void GoToFirstPage() 
        {
            _itemParameters.PageNumber = 1;
            RefreshItems();            
        }

        private  void GoToLastPage()
        {
            _itemParameters.PageNumber = _metaData.TotalPages;
        }

        private void GoToPreviousPage() 
        { 
            if (_metaData.HasPrevious)
            {
                _itemParameters.PageNumber = _metaData.CurrentPage - 1;
                RefreshItems();
            }
        }

        private void GoToNextPage() 
        {
            if (_metaData.HasNext)
            {
                _itemParameters.PageNumber=_metaData.CurrentPage + 1;
                RefreshItems();
            }
        }        
    }
}
