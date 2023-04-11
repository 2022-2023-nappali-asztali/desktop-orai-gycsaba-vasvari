using APIHelpersLibrary.Paged;
using KretaCommandLine.API;
using KretaCommandLine.Model.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KretaDesktop.Services
{
    public interface IAPIService
    {
        public ValueTask<PagingResponse<TEntity>> GetPageAsync<TEntity>(PagingParameters parameters) where TEntity : ClassWithId, new();
        public ValueTask<List<TEntity>> SelectAllRecordAsync<TEntity>() where TEntity : ClassWithId, new();
        public ValueTask<TEntity> GetBy<TEntity>(long id) where TEntity : ClassWithId, new();
        public ValueTask<APICallState> Save<TEntity>(TEntity item) where TEntity : ClassWithId, new();
        public ValueTask<APICallState> Delete<TEntity>(long id) where TEntity : ClassWithId, new();
        public ValueTask<List<TEntity>> SelectAllIncludedRecordAsync<TEntity>() where TEntity : ClassWithId, new ();
        public ValueTask<PagingResponse<TEntity>> SelectAllIncludedRecordPagedAsync<TEntity>(PagingParameters parameters) where TEntity : ClassWithId, new();
    }
}
