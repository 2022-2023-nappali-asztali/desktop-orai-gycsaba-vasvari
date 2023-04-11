using APIHelpersLibrary.Paged;
using KretaCommandLine.Model;
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

        public async ValueTask<PagedList<TEntity>> SelectAllIncludedRecordPagedAsync<TEntity>(PagingParameters parameters) where TEntity : Student, new()
        {
            var dbContext = _dbContextFactory.CreateDbContext();

            IQueryable<TEntity>? entities = GetAllIncluded<TEntity>();

            if (entities is not object)
                return new PagedList<TEntity>();
            else
            {
                List<TEntity> students = await entities.ToListAsync();
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
