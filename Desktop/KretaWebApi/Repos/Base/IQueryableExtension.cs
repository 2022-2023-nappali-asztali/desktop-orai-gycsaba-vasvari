using KretaCommandLine.QueryParameter;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection;
using System.Text;

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

        public static IQueryable<TEntity> ApplySort<TEntity>(this IQueryable<TEntity> entities, string orderByQueryString) where TEntity : class
        {
            if (!entities.Any())
                return entities;
            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                return entities;
            }
            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(TEntity).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();
            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;
                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));
                if (objectProperty == null)
                    continue;
                var sortingOrder = param.EndsWith(" desc") ? "descending" : "ascending";
                orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {sortingOrder}, ");
            }
            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
            return entities.OrderBy(orderQuery);
        }

    }
}
