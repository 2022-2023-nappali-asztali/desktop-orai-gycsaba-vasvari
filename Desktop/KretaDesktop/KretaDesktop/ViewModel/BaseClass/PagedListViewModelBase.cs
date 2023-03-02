using KretaCommandLine.APIModel;
using KretaCommandLine.Model.Abstract;

namespace KretaDesktop.ViewModel.BaseClass
{
    public abstract class PagedListViewModelBase<TEntity> : ViewModelBase, IPagedListViewModelBase<TEntity>
        where TEntity : ClassWithId,new()
    {
        protected PagedList<TEntity> PagedList { get; set; }
        protected QueryStringParameters QueryStringParameters { get; set; }

        // API műveletek -> lekérjük a backendről pl. az első oldalt

        protected void GetPage()
        {
            if (QueryStringParameters.CurrentPage<QueryStringParameters.NumberOfPage)
            {
               // A backendről lekérjük az oldal adatit
            }
        }


    }
}
