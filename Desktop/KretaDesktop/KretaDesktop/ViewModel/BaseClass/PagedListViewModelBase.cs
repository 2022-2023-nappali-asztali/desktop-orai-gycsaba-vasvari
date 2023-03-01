using KretaCommandLine.APIModel;
using KretaCommandLine.Model.Abstract;

namespace KretaDesktop.ViewModel.BaseClass
{
    public abstract class PagedListViewModelBase<TEntity> : ViewModelBase, IPagedListViewModelBase<TEntity>
        where TEntity : ClassWithId,new()
    {
        protected PagedList<TEntity> PagedList { get; set; }

        // API műveletek -> lekérjük a backendről pl. az első oldalt

        public PagedList<TEntity> GetPage(QueryStringParameters queryStringParameters)
        {
            return null;
        }


    }
}
