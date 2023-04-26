using APIHelpersLibrary.Paged;
using KretaCommandLine.Model;
using KretaCommandLine.Model.Abstract;
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

        public async ValueTask<List<TEntity>> SelectEntitysIncludedAsync<TEntity>(long schoolClassId) where TEntity : ClassWithId, new()
        {
            var dbContext = _dbContextFactory.CreateDbContext();

            IQueryable<TEntity>? entities = GetAllIncluded<TEntity>();

            if (entities is not object)
                return new List<TEntity>();
            else
            {
                var search = await entities.SearchById("SchoolClassId", schoolClassId);
                if (search is object)
                    return search;

            }
            return new List<TEntity>();
        }


        public async ValueTask<List<TEntity>> SelectStudentOfClass<TEntity>(long schoolClassId) where TEntity : Student, new()
        {
            var dbContext = _dbContextFactory.CreateDbContext();

            IQueryable<TEntity>? entities = GetAllIncluded<TEntity>();

            if (entities is not object)
                return new List<TEntity>();
            else
            {
                var search = await entities.SearchById("SchoolClassId", schoolClassId);
                if (search is object)
                    return search;

            }

            return new List<TEntity>();
        }

        protected override IQueryable<TEntity>? GetAllIncluded<TEntity>() where TEntity : class 
        {
            return GetAllIncludedStudent<TEntity>();
        }

        protected IQueryable<TEntity>? GetAllIncludedStudent<TEntity>() where TEntity : class
        {
            DbSet<Student>? entities = DbSet<Student>();
            if (entities is not object)
                return null;
            else
            {
                var result = entities
                    .Include(student => student.SchoolClassOfStudent)
                    .Include(student => student.StudentAddress);
                return (IQueryable<TEntity>?)result;
            }
        }
    }
}
