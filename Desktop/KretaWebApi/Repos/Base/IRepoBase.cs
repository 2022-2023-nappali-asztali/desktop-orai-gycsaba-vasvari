using APIHelpersLibrary.Paged;
using KretaCommandLine.API;
using KretaCommandLine.Model.Abstract;
using KretaCommandLine.QueryParameter;

namespace KretaWebApi.Repos.Base
{
    public interface IRepoBase
    {
        public ValueTask<List<TEntity>> SelectAllRecordAsync<TEntity>(QueryParameters? queryParameters) where TEntity : class, new();
        public ValueTask<PagedList<TEntity>> GetPaged<TEntity>(PagingParameters parameters, QueryParameters? queryParameters) where TEntity : class, new();
        public ValueTask<APICallState> UpdateItem<TEntity>(TEntity itemToSave) where TEntity : class, new();
        public ValueTask<APICallState> AddNewItem<TEntity>(TEntity itemToSave) where TEntity : class, new();
        public ValueTask<APICallState> Delete<TEntity>(TEntity entityToDelete) where TEntity : class, new();

        public ValueTask<List<TEntity>> SearchByIdAsync<TEntity>(string propertyName, long id) where TEntity : class, new();

        public int GetNumberOfEntity<TEntity>() where TEntity : class, new();
    }
}
