using APIHelpersLibrary.Paged;
using KretaCommandLine.API;
using KretaCommandLine.Model.Abstract;
using KretaCommandLine.QueryParameter;
using Microsoft.EntityFrameworkCore;

namespace KretaWebApi.Repos.Base
{
    public class RepoBase<TDbContext> : IRepoBase where TDbContext : DbContext
    {
        private IDbContextFactory<TDbContext> _dbContextFactory;

        public RepoBase(IDbContextFactory<TDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async ValueTask<List<TEntity>> SelectAllRecordAsync<TEntity>(QueryParameters? queryParameters) where TEntity : class, new()
        {
            var dbContext = _dbContextFactory.CreateDbContext();
            if (queryParameters == null || (string.IsNullOrEmpty(queryParameters.SearchPropertyName) && string.IsNullOrEmpty(queryParameters.SearchTerm) && string.IsNullOrEmpty(queryParameters.OrderBy)))
            {
                return await dbContext
                    .GetDbSet<TEntity>()
                    .AsNoTracking()
                    .ToListAsync() ?? new List<TEntity>();
            }
            else
            {
                if (string.IsNullOrEmpty(queryParameters.OrderBy))
                {
                    List<TEntity>? result = await dbContext
                        .GetDbSet<TEntity>()
                        .AsQueryable()                        
                        .FiltringAndSorting<TEntity>(queryParameters);
                    if (result is object)
                        return result;                                          
                }
                else
                {
                    if (dbContext is object)
                    {
                        List<TEntity>? filtring = await dbContext
                         .GetDbSet<TEntity>()
                         .AsQueryable()
                         .FiltringAndSorting<TEntity>(queryParameters);
                        return filtring;

                    }
                }
            }
            
            return new List<TEntity>();
        }

        public async ValueTask<PagedList<TEntity>> GetPaged<TEntity>(PagingParameters parameters, QueryParameters? queryParameters) where TEntity : class, new()
        {
            List<TEntity> items = await SelectAllRecordAsync<TEntity>(queryParameters);
            return PagedList<TEntity>.ToPagedList(items, parameters.PageNumber, parameters.PageSize);
        }

        public async ValueTask<APICallState> UpdateItem<TEntity>(TEntity itemToSave) where TEntity : class, new()
        {
            var dbContext = _dbContextFactory.CreateDbContext();
            var dbSet = dbContext.GetDbSet<TEntity>();

            dbContext.ChangeTracker.Clear();
            dbContext.Entry(itemToSave).State = EntityState.Modified;
            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return await ValueTask.FromResult(APICallState.SaveFaild);
            }
            return await ValueTask.FromResult(APICallState.Success);
        }

        public virtual async ValueTask<APICallState> AddNewItem<TEntity>(TEntity itemToSave) where TEntity : class, new()
        {
            var dbContext = _dbContextFactory.CreateDbContext();
            var dbSet = dbContext.GetDbSet<TEntity>();

            dbSet.Add(itemToSave);
            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return await ValueTask.FromResult(APICallState.SaveFaild);
            }
            return await ValueTask.FromResult(APICallState.Success);
        }

        public int GetNumberOfEntity<TEntity>() where TEntity : class, new()
        {
            var dbContext = _dbContextFactory.CreateDbContext();
            DbSet<TEntity> dbSet =  dbContext.GetDbSet<TEntity>();
            int number = dbSet.Count();
            return number;
        }

        protected DbSet<TEntity>? DbSet<TEntity>() where TEntity : class, new()
        {
            var dbContext = _dbContextFactory.CreateDbContext();
            return dbContext.GetDbSet<TEntity>();
        }
    }
}
