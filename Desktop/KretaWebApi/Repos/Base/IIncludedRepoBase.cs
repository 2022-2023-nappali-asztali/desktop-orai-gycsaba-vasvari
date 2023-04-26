using APIHelpersLibrary.Paged;
using KretaCommandLine.Model;
using KretaCommandLine.Model.Abstract;
using KretaCommandLine.QueryParameter;

namespace KretaWebApi.Repos.Base
{
    public interface IIncludedRepoBase :  IRepoBase
    {
        public ValueTask<List<TEntity>> SelectAllIncludedRecordAsync<TEntity>(QueryParameters queryParameters) where TEntity : ClassWithId, new();
        public ValueTask<PagedList<TEntity>> SelectAllIncludedRecordPagedAsync<TEntity>(PagingParameters parameters, QueryParameters? queryParameters) where TEntity : ClassWithId, new();
    }
}
