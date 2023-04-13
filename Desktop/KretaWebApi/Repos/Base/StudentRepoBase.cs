using APIHelpersLibrary.Paged;
using KretaCommandLine.Model;
using KretaCommandLine.QueryParameter;
using Microsoft.AspNetCore.Mvc;
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
                    if (queryParameters is not null && (queryParameters.SearchTerm != null || queryParameters.OrderBy != null))
                        return await query.FiltringAndSorting(queryParameters);
                    else
                        return await query.ToListAsync();
                }
            }
            return new List<TEntity>();
        }

        public async ValueTask<PagedList<TEntity>> SelectAllIncludedRecordPagedAsync<TEntity>(PagingParameters parameters, QueryParameters? queryParameters) where TEntity : Student, new()
        {
            var dbContext = _dbContextFactory.CreateDbContext();

            IQueryable<TEntity>? entities = GetAllIncluded<TEntity>();

            if (entities is not object)
                return new PagedList<TEntity>();
            else
            {
                List<TEntity> students = new List<TEntity>();
                if (queryParameters is not null && (queryParameters.SearchTerm != null || queryParameters.OrderBy != null))
                     students = await entities.FiltringAndSorting(queryParameters);
                else
                    students= await entities.ToListAsync();
                return PagedList<TEntity>.ToPagedList(students, parameters.PageNumber, parameters.PageSize); ;
            }
        }

        public async ValueTask<List<TEntity>> SelectStudentOfClass<TEntity>(long schoolClassId) where TEntity : Student, new()
        {
            var dbContext = _dbContextFactory.CreateDbContext();

            IQueryable<TEntity>? entities = GetAllIncluded<TEntity>();

            if (entities is not object)
                return new List<TEntity>();
            else
            {
                return await entities.SearchById<TEntity>("SchoolClassId", schoolClassId);
            }
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
