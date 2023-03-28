using APIHelpersLibrary.Paged;
using KretaCommandLine.API;
using KretaCommandLine.Model.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KretaDesktop.Services
{
    public interface IAPIService
    {
        public Task<PagingResponse<TEntity>> GetPageAsync<TEntity>(ItemParameters parameters) where TEntity : ClassWithId, new();
        public ValueTask<List<TEntity>> SelectAllRecordAsync<TEntity>() where TEntity : ClassWithId, new();
        public ValueTask<TEntity> GetBy<TEntity>(long id) where TEntity : ClassWithId, new();
        public ValueTask<APICallState> Save<TEntity>(TEntity item) where TEntity : ClassWithId, new();
        public ValueTask<APICallState> Delete<TEntity>(long id) where TEntity : ClassWithId, new();
    }
}
