using APIHelpersLibrary.Paged;
using KretaCommandLine.API;
using KretaCommandLine.Model.Abstract;
using Microsoft.EntityFrameworkCore;

namespace KretaWebApi.Repos
{
    public class RepoBase<TDbContext> : IRepoBase where TDbContext : DbContext
    {
        private IDbContextFactory<TDbContext> _dbContextFactory;

        public RepoBase(IConfiguration configuration, IDbContextFactory<TDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async ValueTask<List<TEntity>> SelectAllRecordAsync<TEntity>() where TEntity : ClassWithId, new()
        {
            var dbContext = _dbContextFactory.CreateDbContext();
            return await dbContext.GetDbSet<TEntity>()
                                    .AsNoTracking<TEntity>()
                                    .ToListAsync<TEntity>() ?? new List<TEntity>();
        }

        public async Task<PagedList<TEntity>> GetPaged<TEntity>(ItemParameters parameters) where TEntity : ClassWithId, new()
        {
            List<TEntity> items = await SelectAllRecordAsync<TEntity>();
            return PagedList<TEntity>.ToPagedList(items, parameters.PageNumber, parameters.PageSize);
        }

        public async ValueTask<TEntity> GetBy<TEntity>(long id) where TEntity : ClassWithId, new()
        {

            var dbContext = _dbContextFactory.CreateDbContext();
            var dbSet = dbContext.GetDbSet<TEntity>();
            var result = await dbSet.FirstOrDefaultAsync(entity => entity.Id == id) ?? new TEntity();

            return result;
        }

        public async ValueTask<APICallState> Save<TEntity>(TEntity itemToSave) where TEntity : ClassWithId, new()
        {

            if (((ClassWithId)itemToSave).HasId)
            {
                return await UpdateItem<TEntity>(itemToSave);
            }
            else
            {
                return await AddNewItem<TEntity>(itemToSave);
            }
        }

        public async ValueTask<APICallState> Delete<TEntity>(long id) where TEntity : ClassWithId, new()
        {

            var dbContext = _dbContextFactory.CreateDbContext();
            var dbSet = dbContext.GetDbSet<TEntity>();
            TEntity entityToDelete = await GetBy<TEntity>(id);
            if (entityToDelete != null && entityToDelete != default)
            {
                try
                {
                    dbContext.ChangeTracker.Clear();
                    dbContext.Entry(entityToDelete).State = EntityState.Deleted;
                    await dbContext.SaveChangesAsync();
                }
                catch (Exception)
                {
                    return await ValueTask.FromResult(APICallState.DeleteFail);
                }
                return await ValueTask.FromResult(APICallState.Success);
            }
            return await ValueTask.FromResult(APICallState.DeleteFail);
        }

        private async ValueTask<APICallState> UpdateItem<TEntity>(TEntity itemToSave) where TEntity : ClassWithId, new()
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

        private async ValueTask<APICallState> AddNewItem<TEntity>(TEntity itemToSave) where TEntity : ClassWithId, new()
        {
            var dbContext = _dbContextFactory.CreateDbContext();
            var dbSet = dbContext.GetDbSet<TEntity>();

            itemToSave.Id = GetNextId<TEntity>();

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

        private long GetNextId<TEntity>() where TEntity : ClassWithId, new()
        {
            var dbContext = _dbContextFactory.CreateDbContext();
            var dbSet = dbContext.GetDbSet<TEntity>();

            long maxId = dbSet.Select(entity => entity.Id).Max();
            if (maxId < 0)
                maxId = 0;
            return maxId + 1;
        }
    }
}
