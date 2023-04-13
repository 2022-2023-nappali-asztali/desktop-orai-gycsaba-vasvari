using KretaCommandLine.Model.Abstract;
using KretaDesktop.Services;
using KretaDesktop.ViewModel.Command;
using System.Threading.Tasks;

namespace KretaDesktop.ViewModel.BaseClass
{
    public class SearchAndSortViewModel<TEntity> : ListViewModel<TEntity> where TEntity : ClassWithId, new()
    {
        private string _searchedPropertyName = string.Empty;
        protected string SearchedPropertyName
        {
            get =>_searchedPropertyName;
            set => SetValue(ref _searchedPropertyName, value);
        }

        private string _searchTerm = string.Empty;
        public string SearchTerm
        {
            get => _searchTerm;
            set => SetValue(ref _searchTerm, value);
        }

        public SearchAndSortViewModel(IAPIService service) : base(service)
        {
            FilterItemsCommand = new AsyncRelayCommand(OnFilterItems, (ex) => OnException());
        }

        public AsyncRelayCommand FilterItemsCommand { get; set; }

        protected async Task OnFilterItems()
        {
            await SearchAndSortItems(_searchedPropertyName, _searchTerm,"");
        }

        private void OnException()
        {
        }
    }
}
