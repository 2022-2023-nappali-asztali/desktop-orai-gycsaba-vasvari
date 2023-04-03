using APIHelpersLibrary.Paged;
using KretaDesktop.ViewModel.Command;

namespace KretaDesktop.ViewModel.BaseClass.Interface
{
    public interface IPaged<TEntity>
    {
        public AsyncRelayCommand FirstPageCommand { get; }
        public AsyncRelayCommand PreviousPageCommand { get;  }
        public AsyncRelayCommand NextPageCommand { get;  }
        public AsyncRelayCommand LastPageCommand { get; }

        public MetaData MetaData { get; set; }

        public string PageInformation { get; }
    }
}
