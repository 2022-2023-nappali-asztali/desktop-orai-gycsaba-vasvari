using APIHelpersLibrary.Paged;
using KretaCommandLine.Model;
using KretaCommandLine.QueryParameter;
using KretaWebApi.Repos.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KretaWebApi.Repos
{
    public class StudentRepoBase<TDbContext> : RepoBase<TDbContext>, IStudentRepoBase where TDbContext : DbContext

    {
        private IDbContextFactory<TDbContext> _dbContextFactory;

        public StudentRepoBase(IDbContextFactory<TDbContext> dbContextFactory) : base(dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async ValueTask<List<TEntity>> SelectAllIncludedRecordAsync<TEntity>(QueryParameters queryParameters) where TEntity : Student, new()
        {
            var dbContext = _dbContextFactory.CreateDbContext();

            DbSet<TEntity>? entities = DbSet<TEntity>();

            if (entities is not object)
                return new List<TEntity>();
            else
            {
                IQueryable<TEntity>? query = GetAllIncluded<TEntity>();
                if (query is not null)
                {
                    if (query is object && queryParameters is not null)
                    {
                        List<TEntity>? result = await query.FiltringAndSorting(queryParameters);
                        return result;
                    }
                    else
                    {
                        if (query is object)
                            return await query.ToListAsync();
                    }
                }
            }
            return new List<TEntity>();
        }

        public async ValueTask<PagedList<TEntity>> SelectAllIncludedRecordPagedAsync<TEntity>(PagingParameters parameters, QueryParameters? queryParameters) where TEntity : Student, new()
        {
            //var dbContext = _dbContextFactory.CreateDbContext();

            IQueryable<TEntity>? entities = GetAllIncluded<TEntity>();


            List<TEntity> students = new List<TEntity>();
            if (entities is object && queryParameters is not null)
            {
                List<TEntity>? result = await entities.FiltringAndSorting(queryParameters);
                students = result;
            }
            else
            {
                if (entities is object)
                    students = await entities.ToListAsync();
                else
                    students = new List<TEntity>();
            }
            return PagedList<TEntity>.ToPagedList(students, parameters.PageNumber, parameters.PageSize); ;
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

        private IQueryable<TEntity>? GetAllIncluded<TEntity>() where TEntity : Student, new()
        {
            DbSet<TEntity>? entities = DbSet<TEntity>();
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
