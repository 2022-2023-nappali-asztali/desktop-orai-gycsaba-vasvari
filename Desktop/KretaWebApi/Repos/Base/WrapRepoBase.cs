using APIHelpersLibrary.Paged;
using KretaCommandLine.API;
using KretaCommandLine.Model.Abstract;
using KretaCommandLine.QueryParameter;
using Microsoft.EntityFrameworkCore;

namespace KretaWebApi.Repos.Base
{
    public class WrapRepoBase<TDbContext> : IWrapRepoBase where TDbContext : DbContext
    {
        private IDbContextFactory<TDbContext> _dbContextFactory;

        public WrapRepoBase(IDbContextFactory<TDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public DbSet<TEntity>? DbSet<TEntity>() where TEntity : class, new()
        {
            var dbContext = _dbContextFactory.CreateDbContext();
            return dbContext.GetDbSet<TEntity>();
        }

    }
}
