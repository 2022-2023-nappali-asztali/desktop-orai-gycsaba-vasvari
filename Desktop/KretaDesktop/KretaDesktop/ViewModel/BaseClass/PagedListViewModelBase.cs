using KretaCommandLine.APIModel;
using KretaCommandLine.Model.Abstract;
using KretaDesktop.Services;
using System.Threading.Tasks;

namespace KretaDesktop.ViewModel.BaseClass
{
    public abstract class PagedListViewModelBase<TEntity> : ViewModelBase, IPagedListViewModelBase<TEntity>
        where TEntity : ClassWithId,new()
    {
        protected PagedList<TEntity> PagedList { get; set; }
        protected QueryStringParameters QueryStringParameters { get; set; } = new QueryStringParameters();

        CURDAPIService service = new CURDAPIService();

        protected async Task GetPageAsync()
        {
            if (QueryStringParameters.CurrentPage<QueryStringParameters.NumberOfPage)
            {
                PagedList = await service.GetPageAsync<TEntity>(QueryStringParameters);
                
            }
        }
    }
}
