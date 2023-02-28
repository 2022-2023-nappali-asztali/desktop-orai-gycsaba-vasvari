using KretaCommandLine.APIModel;
using KretaCommandLine.Model.Abstract;

namespace KretaDesktop.ViewModel.BaseClass
{
    public class PagedListViewModelBase<TEntity,TPagedList> : ViewModelBase<TEntity,TPagedList>
        where TEntity : ClassWithId,new()
        where TPagedList : PagedList<TEntity>, new()
    {
    }
}
