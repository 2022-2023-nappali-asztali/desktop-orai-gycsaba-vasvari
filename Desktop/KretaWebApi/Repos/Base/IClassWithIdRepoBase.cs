using APIHelpersLibrary.Paged;
using KretaCommandLine.API;
using KretaCommandLine.Model.Abstract;
using KretaCommandLine.QueryParameter;

namespace KretaWebApi.Repos.Base
{
    public interface IClassWithIdRepoBase : IRepoBase
    {
        public ValueTask<TEntity> GetBy<TEntity>(long id) where TEntity : ClassWithId, new();
        public ValueTask<APICallState> Save<TEntity>(TEntity item) where TEntity : ClassWithId, new();
        public ValueTask<APICallState> Delete<TEntity>(long id) where TEntity : ClassWithId, new();
        public ValueTask<APICallState> AddNewClassWithIdItem<TEntity>(TEntity itemToSave) where TEntity : ClassWithId, new();
    }
}
