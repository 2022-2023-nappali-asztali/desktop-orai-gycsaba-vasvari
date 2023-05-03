using APIHelpersLibrary.Paged;
using KretaCommandLine.API;
using KretaCommandLine.Model.Abstract;
using KretaCommandLine.QueryParameter;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KretaDesktop.Services
{
    public interface IAPIService
    {
        public ValueTask<PagingResponse<TEntity>> GetPageAsync<TEntity>(PagingParameters pagingParameters, QueryParameters? queryParameters=null) where TEntity : ClassWithId, new();
        public ValueTask<List<TEntity>> SelectAllRecordAsync<TEntity>(QueryParameters? queryParameters=null) where TEntity : ClassWithId, new();
        public ValueTask<TEntity> GetBy<TEntity>(long id) where TEntity : ClassWithId, new();

        public ValueTask<APICallState> Save<TEntity>(TEntity item) where TEntity : ClassWithId, new();

        public ValueTask<APICallState> SaveNewEntity<TEntity>(TEntity item) where TEntity : class, new();
        public ValueTask<APICallState> Delete<TEntity>(long id) where TEntity : ClassWithId, new();
        public ValueTask<APICallState> DeleteObject<TEntity>(TEntity item) where TEntity : class, new();

        public ValueTask<List<TEntity>> SelectAllIncludedRecordAsync<TEntity>(QueryParameters? queryParameters=null) where TEntity : ClassWithId, new ();
        public ValueTask<PagingResponse<TEntity>> SelectAllIncludedRecordPagedAsync<TEntity>(PagingParameters parameters, QueryParameters? queryParameters = null) where TEntity : ClassWithId, new();
        public ValueTask<int> GetCountOf<TEntity>() where TEntity : ClassWithId, new();
    }
}
