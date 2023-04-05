using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace KretaWebApi.Repos.Base
{
    public static class IQueryableExtension
    {
        public async static ValueTask<List<TEntity>> SearchById<TEntity>(this IQueryable<TEntity> dbSet, string propertyName, long id) where TEntity : class
        {
            List<TEntity> result = new List<TEntity>();
            if (dbSet.Any() && !string.IsNullOrEmpty(propertyName))
            {
                PropertyInfo? propertyInfo = dbSet.First().GetType().GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfo is object)
                {
                    try
                    {
                        var list = await dbSet.ToListAsync();
                        result = list.Where(entity => (long)propertyInfo.GetValue(entity, null) == id).ToList();
                    }
                    catch (Exception e)
                    {
                    }
                }
            }
            return result;
        }
    }
}
