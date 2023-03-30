using APIHelpersLibrary.Paged;
using KretaCommandLine.API;
using KretaCommandLine.Model.Abstract;

namespace KretaWebApi.Repos.Base
{
    public interface IRepoBase
    {
        public ValueTask<List<TEntity>> SelectAllRecordAsync<TEntity>() where TEntity : ClassWithId, new();
        public ValueTask<TEntity> GetBy<TEntity>(long id) where TEntity : ClassWithId, new();
        public ValueTask<APICallState> Save<TEntity>(TEntity item) where TEntity : ClassWithId, new();
        public ValueTask<APICallState> Delete<TEntity>(long id) where TEntity : ClassWithId, new();

        public ValueTask<PagedList<TEntity>> GetPaged<TEntity>(ItemParameters parameters) where TEntity : ClassWithId, new();
    }
}
