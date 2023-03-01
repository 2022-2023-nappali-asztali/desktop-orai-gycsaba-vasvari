using KretaCommandLine.APIModel;

namespace KretaDesktop.ViewModel.BaseClass
{
    public interface IPagedListViewModelBase<TEntity>
    {
        public PagedList<TEntity> GetPage(QueryStringParameters queryStringParameters);
    }
}
