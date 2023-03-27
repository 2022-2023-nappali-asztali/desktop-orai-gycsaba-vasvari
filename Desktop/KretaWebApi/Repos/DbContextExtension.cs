using KretaCommandLine.Model.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace KretaWebApi.Repos
{
    public static class DbContextExtension
    {
        public static DbSet<TEntity> GetDbSet<TEntity>(this DbContext context) where TEntity : ClassWithId, new() 
        {
            string dbSetName = new TEntity().GetType().Name;
            PropertyInfo? propertyInfo = context.GetType().GetProperty(dbSetName);
            if (propertyInfo is not object)
                throw new InvalidOperationException($"{dbSetName} kiolvasása sikertelen.");

            DbSet<TEntity>? set = null;
            try
            {
                var setValue = propertyInfo.GetValue(context);
                if (setValue is object)
                {
                    set = (DbSet<TEntity>)setValue;
                }
                else
                    throw new InvalidOperationException($"{dbSetName} adattáblája nem megfelelő típusú.");
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"{dbSetName} -nek nincs adatbázis táblája.");
            }
            if (set != null)
            {
                return set;
            }
            else
                throw new InvalidOperationException($"{dbSetName} -nek nincs adatbázis tábla kiolvasása nem sikerült.");

        }
    }
}
