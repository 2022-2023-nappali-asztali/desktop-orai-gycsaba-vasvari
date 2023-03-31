using KretaCommandLine.Model;

namespace KretaWebApi.Repos.Base
{
    public interface IStudentRepoBase : IRepoBase
    {
        public ValueTask<List<TEntity>> SelectAllIncludedRecordAsync<TEntity>() where TEntity : Student, new();
    }
}
