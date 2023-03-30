using KretaDesktop.ViewModel.Command;

namespace KretaDesktop.ViewModel.BaseClass.Interface
{
    public interface IPagedListViewModelBase<TEntity>
    {
        public AsyncRelayCommand FirstPageCommand { get; }
        public AsyncRelayCommand PreviousPageCommand { get;  }
        public AsyncRelayCommand NextPageCommand { get;  }
        public AsyncRelayCommand LastPageCommand { get; }

        public string PageInformation { get; }
    }
}
