using APIHelpersLibrary.Paged;
using KretaCommandLine.Model;
using KretaCommandLine.Model.Abstract;
using KretaCommandLine.QueryParameter;
using KretaWebApi.Repos.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KretaWebApi.Repos
{
    public class SchoolClassRepoBase<TDbContext> : IncludedRepoBase<TDbContext> where TDbContext : DbContext

    {
        private IDbContextFactory<TDbContext> _dbContextFactory;

        public SchoolClassRepoBase(IDbContextFactory<TDbContext> dbContextFactory) : base(dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async ValueTask<List<TEntity>> SelectSchoolClassIncludedAsync<TEntity>(long schoolClassId) where TEntity : ClassWithId, new()
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
            return GetAllIncludedSchoolClass<TEntity>();
        }

        protected IQueryable<TEntity>? GetAllIncludedSchoolClass<TEntity>() where TEntity : class
        {
            DbSet<SchoolClass>? entities = DbSet<SchoolClass>();
            if (entities is not object)
                return null;
            else
            {
                var result = entities
                    .Include(schoolClass => schoolClass.HeadTeacher)
                    .Include(SchoolClass => SchoolClass.Students);

                return (IQueryable<TEntity>?)result;
            }
        }
    }
}
