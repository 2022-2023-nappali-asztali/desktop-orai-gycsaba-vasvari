using KretaCommandLine.Model.Abstract;
using KretaDesktop.Services;
using KretaDesktop.ViewModel.Command;
using System.Threading.Tasks;

namespace KretaDesktop.ViewModel.BaseClass
{
    public class SearchAndSortViewModel<TEntity> : ListViewModel<TEntity> where TEntity : ClassWithId, new()
    {
        private string _searchedPropertyName = string.Empty;
        protected string SearchedPropertyName;

        private string _itemFilter = string.Empty;
        protected string ItemFilter
        {
            get => _itemFilter;
            set => SetValue(ref _itemFilter, value);
        }

        public SearchAndSortViewModel(IAPIService service) : base(service)
        {
            FilterItemsCommand = new AsyncRelayCommand(OnFilterItems, (ex) => OnException());
        }

        public AsyncRelayCommand FilterItemsCommand { get; set; }

        protected async Task OnFilterItems()
        {
            await FilterItems(_itemFilter);
        }

        private void OnException()
        {
        }
    }
}
