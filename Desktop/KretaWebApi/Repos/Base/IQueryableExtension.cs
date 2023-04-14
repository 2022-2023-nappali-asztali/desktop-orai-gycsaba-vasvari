using KretaCommandLine.QueryParameter;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection;

namespace KretaWebApi.Repos.Base
{
    public static class IQueryableExtension
    {
        public static IQueryable<TEntity>? SearchById<TEntity>(this IQueryable<TEntity> dbSet, string propertyName, long id) where TEntity : class
        {
            if (dbSet.Any() && !string.IsNullOrEmpty(propertyName))
            {
                PropertyInfo? propertyInfo = dbSet.First().GetType().GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfo is object)
                {
                    try
                    {
                        var result = dbSet.Where(entity => (long)propertyInfo.GetValue(entity, null) == id);
                        return result;
                    }
                    catch (Exception e)
                    {
                    }
                }
            }
            return null;
        }
    }
}
