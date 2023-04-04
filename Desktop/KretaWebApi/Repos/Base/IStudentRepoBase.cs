using APIHelpersLibrary.Paged;
using KretaCommandLine.Model;

namespace KretaWebApi.Repos.Base
{
    public interface IStudentRepoBase : IRepoBase
    {
        public ValueTask<List<TEntity>> SelectAllIncludedRecordAsync<TEntity>() where TEntity : Student, new();
        public ValueTask<PagedList<TEntity>> SelectAllIncludedRecordPagedAsync<TEntity>(ItemParameters parameters) where TEntity : Student, new();
        public ValueTask<List<TEntity>> SelectStudentOfClass<TEntity>(long schoolClassId) where TEntity : Student, new();
    }
}
