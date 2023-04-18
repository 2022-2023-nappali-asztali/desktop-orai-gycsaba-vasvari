using KretaCommandLine.Model.Abstract;
using KretaCommandLine.QueryParameter;
using KretaDesktop.Services;
using KretaDesktop.ViewModel.Command;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
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

        private List<SortingData>? _sortingDatas = null;
        protected List<SortingData>? SortingDatas 
        {
            get => _sortingDatas;
            set
            {
                SetValue(ref _sortingDatas, value);
                OnPropertyChanged(nameof(SortingLabels));
            }
        }

        private string _sortingCommand= string.Empty;
        private string _selectedSortingLabel;
        public string SelectedSortingLabel
        {
            get => _selectedSortingLabel;
            set
            {
                SetValue(ref _selectedSortingLabel, value);
                _sortingCommand= _sortingDatas.Where(data => data.SortingLabel == SelectedSortingLabel).Select(data => data.SortingCommand).FirstOrDefault();
                OnSortAndFilterItems();
            }
        }

        public ObservableCollection<string> SortingLabels
        {
            get =>new ObservableCollection<string>(_sortingDatas.Select(sortingdata => sortingdata.SortingLabel));
        }

        private string _searchTerm = string.Empty;
        public string SearchTerm
        {
            get => _searchTerm;
            set => SetValue(ref _searchTerm, value);
        }

        public SearchAndSortViewModel(IAPIService service) : base(service)
        {
            SearchItemsCommand = new AsyncRelayCommand(OnSortAndFilterItems, (ex) => OnException());
            ShowAllItemsCommand = new AsyncRelayCommand(OnShowAllItems, (ex) => OnException());
        }

        public AsyncRelayCommand SearchItemsCommand { get; set; }
        public AsyncRelayCommand ShowAllItemsCommand { get; set; }

        protected async Task OnSortAndFilterItems()
        {
            await SearchAndSortItems(_searchedPropertyName, _searchTerm, _sortingCommand);
        }

        protected async Task OnShowAllItems()
        {
            SearchTerm = string.Empty;
            _searchedPropertyName= string.Empty;
            await SearchAndSortItems(_searchedPropertyName, _searchTerm, _sortingCommand);
        }

        private void OnException()
        {
        }
    }
}
