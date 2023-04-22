using KretaCommandLine.QueryParameter;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text;

using System.Linq.Dynamic.Core;

namespace KretaWebApi.Repos.Base
{

    // https://daedtech.com/linq-order-by-when-you-have-property-name/

    public static class DbSetExtension
    {
        public async static ValueTask<List<TEntity>> SearchById<TEntity>(this DbSet<TEntity> dbSet, string propertyName, long id) where TEntity : class
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

        public async static ValueTask<List<TEntity>> FiltringAndSorting<TEntity>(this DbSet<TEntity> dbSet, QueryParameters queryParameters) where TEntity : class
        {
            List<TEntity> result = new List<TEntity>();
            if (dbSet.Any() && !string.IsNullOrEmpty(queryParameters.SearchPropertyName))
            {
                PropertyInfo? propertyInfo = dbSet.First().GetType().GetProperty(queryParameters.SearchPropertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfo is object)
                {
                    try
                    {
                        var list = await dbSet.ToListAsync();
                        string lowerCaseSearchTerm = queryParameters.SearchTerm.Trim().ToLower();
                        result = list.Where(entity => propertyInfo.GetValue(entity, null).ToString().ToLower().Contains(lowerCaseSearchTerm)).ApplySort(queryParameters.OrderBy).ToList();
                    }
                    catch (Exception e)
                    {
                    }
                }
            }
            return result;
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

        public static IEnumerable<TEntity> ApplySort<TEntity>(this IEnumerable<TEntity> entities, string orderByQueryString) where TEntity : class
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
            // https://stackoverflow.com/questions/41244/dynamic-linq-orderby-on-ienumerablet-iqueryablet
            return entities.AsQueryable().OrderBy(orderQuery);
        }
    }
}