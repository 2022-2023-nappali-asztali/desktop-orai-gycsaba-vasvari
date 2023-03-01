using KretaCommandLine.APIModel;
using KretaCommandLine.Model.Abstract;

namespace KretaDesktop.ViewModel.BaseClass
{
    public class PagedListViewModelBase<TEntity> : ViewModelBase
        where TEntity : ClassWithId,new()
    {
        protected PagedList<TEntity> PagedList { get; set; }

        // API műveletek -> lekérjük a backendről pl. az első oldalt
    }
}
