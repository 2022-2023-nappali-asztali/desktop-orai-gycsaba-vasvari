using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace KretaWebApi.Repos.Base
{

    // https://daedtech.com/linq-order-by-when-you-have-property-name/

    public static class DbSetExtension
    {
        public static IQueryable<TEntity>? SearchById<TEntity>(this DbSet<TEntity> dbSet,string propertyName, long id ) where TEntity : class
        {
            if (dbSet.Any() && !string.IsNullOrEmpty(propertyName))
            { 
                PropertyInfo? propertyInfo = dbSet.First().GetType().GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfo is object)
                {
                    try
                    {
                        //List<long> list = dbSet.Select(entity => (long) propertyInfo.GetValue(entity, null)).ToList();
                        var result = dbSet.Where<TEntity>(entity => (long)propertyInfo.GetValue(entity, null) == id).ToList();
                    }
                    catch (Exception e) 
                    { 
                    }
                }
            }
           return dbSet; 
        }
    }
}
