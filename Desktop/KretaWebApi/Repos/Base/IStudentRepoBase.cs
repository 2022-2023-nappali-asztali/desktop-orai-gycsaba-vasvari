using APIHelpersLibrary.Paged;
using KretaCommandLine.Model;
using KretaCommandLine.QueryParameter;

namespace KretaWebApi.Repos.Base
{
    public interface IStudentRepoBase :  IRepoBase
    {
        public ValueTask<List<TEntity>> SelectAllIncludedRecordAsync<TEntity>(QueryParameters queryParameters) where TEntity : Student, new();
        public ValueTask<PagedList<TEntity>> SelectAllIncludedRecordPagedAsync<TEntity>(PagingParameters parameters, QueryParameters? queryParameters) where TEntity : Student, new();
        public ValueTask<List<TEntity>> SelectStudentOfClass<TEntity>(long schoolClassId) where TEntity : Student, new();
    }
}
