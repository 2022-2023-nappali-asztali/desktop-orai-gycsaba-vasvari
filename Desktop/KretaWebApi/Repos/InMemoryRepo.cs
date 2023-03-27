using EF.Contexts;
using KretaCommandLine.Model.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KretaWebApi.Repos
{
    public class InMemoryRepo<TEntity> where TEntity : ClassWithId
    {
        private bool _disposed = false;

        protected InMemoryContext  Context  { get; private set; }
        protected DbSet<TEntity> DatabaseSet => Context.Set<TEntity>();


        public InMemoryRepo(InMemoryContext dbContext)
        {
            Context = dbContext;
        }

        public IQueryable<TEntity>? GetAll()
        {

            if (DatabaseSet is null)
                return null;
            else
                return DatabaseSet.AsNoTracking();
        }

        public IQueryable<TEntity>? FindByCondition(Expression<Func<TEntity, bool>> condition)
        {

            if (DatabaseSet is null)
                return null;
            else
                return DatabaseSet.Where(condition).AsNoTracking();
        }

        public TEntity? FindById(long guid)
        {
            foreach (var entity in DatabaseSet)
            {
                if (entity.Id == guid) return entity;
            }
            return null;
        }

        public async Task<TEntity?> Get(long guid)
        {
            if (guid == 0)
            {
                return null;
            }

            if (DatabaseSet is null)
            {
                return null;
            }
            else
            {
                var founded = FindByCondition(entity => entity.Id == guid);
                if (founded is object)
                {
                    return await founded.FirstOrDefaultAsync();
                }
                else
                    return null;
            }
        }

        public async Task Save(TEntity entity)
        {
            if (entity.HasId)
            {
                await Update(entity);
            }
            else
            {
                await Insert(entity);
            }
        }

        public async Task Insert(TEntity entity)
        {
            if (DatabaseSet is object)
            {
                DatabaseSet.Add(entity);

                await Context.SaveChangesAsync();

            }
        }

        public async Task Update(TEntity entity)
        {
            if (DatabaseSet is object && entity is object)
            {
                TEntity? entityToBeModifyd = FindById(entity.Id);

                if (entityToBeModifyd is object)
                {
                    Context.Entry(entity).State = EntityState.Modified;
                    await Context.SaveChangesAsync();
                }
            }
        }

        public void Delete(TEntity entity)
        {
            if (DatabaseSet is object)
            {
                DatabaseSet.Remove(entity);
                Context.SaveChanges();
            }

        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
