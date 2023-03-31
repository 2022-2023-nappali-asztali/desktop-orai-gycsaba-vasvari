using KretaCommandLine.Model;
using Microsoft.EntityFrameworkCore;

namespace KretaWebApi.Repos.Base
{
    public class StudentRepoBase<TDbContext> : RepoBase<TDbContext>, IStudentRepoBase where TDbContext : DbContext

    {
        private IDbContextFactory<TDbContext> _dbContextFactory;

        public StudentRepoBase(IDbContextFactory<TDbContext> dbContextFactory) : base(dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async ValueTask<List<TEntity>> SelectAllIncludedRecordAsync<TEntity>() where TEntity : Student, new()
        {
            var dbContext = _dbContextFactory.CreateDbContext();

            DbSet<TEntity> entities = DbSet<TEntity>();

            if (entities is not object)
                return new List<TEntity>();
            else
            {
                List<TEntity> students = await entities
                    .Include(student => student.SchoolClassOfStudent)
                    .Include(student => student.StudentAddress)
                    .ToListAsync();
                return students;
            }
        }
    }
}
