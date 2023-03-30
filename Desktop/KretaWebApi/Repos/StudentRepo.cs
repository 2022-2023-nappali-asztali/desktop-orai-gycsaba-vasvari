using KretaCommandLine.Model;
using KretaCommandLine.Model.Abstract;
using KretaWebApi.Repos.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace KretaWebApi.Repos
{
    public class StudentRepo<TDbContext> :RepoBase<TDbContext> , IStudentRepo where TDbContext : DbContext

    {
        private IDbContextFactory<TDbContext> _dbContextFactory;

        public StudentRepo(IConfiguration configuration, IDbContextFactory<TDbContext> dbContextFactory) : base(configuration, dbContextFactory)
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
                List<TEntity> students= await entities
                    .Include(student => student.SchoolClassOfStudent)
                    .Include(student => student.StudentAddress)
                    .ToListAsync();
                return students;
            }           
        }
    }
}
