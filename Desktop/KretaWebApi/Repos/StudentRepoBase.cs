using APIHelpersLibrary.Paged;
using KretaCommandLine.Model;
using KretaCommandLine.QueryParameter;
using KretaWebApi.Repos.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KretaWebApi.Repos
{
    public class StudentRepoBase<TDbContext> : IncludedRepoBase<TDbContext>, IStudentRepoBase where TDbContext : DbContext

    {
        private IDbContextFactory<TDbContext> _dbContextFactory;

        public StudentRepoBase(IDbContextFactory<TDbContext> dbContextFactory) : base(dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }


        protected override IQueryable<TEntity>? GetAllIncluded<TEntity>() where TEntity : class 
        {
            return GetAllIncluded();
        }

        protected IQueryable<Student>? GetAllIncluded() 
        {
            DbSet<Student>? entities = DbSet<Student>();
            if (entities is not object)
                return null;
            else
            {
                return entities
                    .Include(student => student.SchoolClassOfStudent)
                    .Include(student => student.StudentAddress);
            }
        }
    }
}
