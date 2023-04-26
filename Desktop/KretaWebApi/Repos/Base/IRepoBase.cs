using APIHelpersLibrary.Paged;
using KretaCommandLine.API;
using KretaCommandLine.Model.Abstract;
using KretaCommandLine.QueryParameter;

namespace KretaWebApi.Repos.Base
{
    public interface IRepoBase
    {
        public ValueTask<List<TEntity>> SelectAllRecordAsync<TEntity>(QueryParameters? queryParameters) where TEntity : ClassWithId, new();
        public ValueTask<TEntity> GetBy<TEntity>(long id) where TEntity : ClassWithId, new();
        public ValueTask<APICallState> Save<TEntity>(TEntity item) where TEntity : ClassWithId, new();
        public ValueTask<APICallState> Delete<TEntity>(long id) where TEntity : ClassWithId, new();
        public ValueTask<PagedList<TEntity>> GetPaged<TEntity>(PagingParameters parameters, QueryParameters? queryParameters) where TEntity : ClassWithId, new();

        public int GetNumberOfEntity<TEntity>() where TEntity : ClassWithId, new();
    }
}
