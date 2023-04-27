using APIHelpersLibrary.Paged;
using KretaCommandLine.API;
using KretaCommandLine.Model.Abstract;
using KretaCommandLine.QueryParameter;
using Microsoft.EntityFrameworkCore;

namespace KretaWebApi.Repos.Base
{
    public class ClassWithIdRepoBase<TDbContext> : RepoBase<TDbContext>, IClassWithIdRepoBase where TDbContext : DbContext
    {
        private IDbContextFactory<TDbContext> _dbContextFactory;

        public ClassWithIdRepoBase(IDbContextFactory<TDbContext> dbContextFactory) : base(dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
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

            if (itemToSave.HasId)
            {
                return await UpdateItem(itemToSave);
            }
            else
            {
                return await AddNewClassWithIdItem(itemToSave);
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

        public async ValueTask<APICallState> AddNewClassWithIdItem<TEntity>(TEntity itemToSave) where TEntity : ClassWithId, new()
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

        protected long GetNextId<TEntity>() where TEntity : ClassWithId, new()
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
