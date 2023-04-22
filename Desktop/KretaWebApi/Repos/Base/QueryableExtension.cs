using KretaCommandLine.QueryParameter;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection;
using System.Text;

using System.Linq.Dynamic.Core;


namespace KretaWebApi.Repos.Base
{
    public static class IQueryableExtension
    {
        public async static ValueTask<List<TEntity>> SearchById<TEntity>(this IQueryable<TEntity> query, string propertyName, long id) where TEntity : class
        {
            List<TEntity> result = new List<TEntity>();
            if (query.Any() && !string.IsNullOrEmpty(propertyName))
            {
                PropertyInfo? propertyInfo = query.First().GetType().GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfo is object)
                {
                    try
                    {
                        var list = await query.ToListAsync();
                        result = list.Where(entity => (long)propertyInfo.GetValue(entity, null) == id).ToList();
                    }
                    catch (Exception e)
                    {
                    }
                }
            }
            return result;
        }

        public async static ValueTask<List<TEntity>> FiltringAndSorting<TEntity>(this IQueryable<TEntity> query, QueryParameters queryParameters) where TEntity : class
        {
            List<TEntity> result = new List<TEntity>();
            if (query.Any() && !string.IsNullOrEmpty(queryParameters.SearchPropertyName))
            {
                PropertyInfo? propertyInfo = query.First().GetType().GetProperty(queryParameters.SearchPropertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfo is object)
                {
                    try
                    {
                        var list = await query.ToListAsync();
                        var lowerCaseSearchTerm = queryParameters.SearchTerm.Trim().ToLower();
                        result = list.Where(entity => propertyInfo.GetValue(entity, null).ToString().ToLower().Contains(lowerCaseSearchTerm)).ToList();
                        if (! string.IsNullOrEmpty(queryParameters.OrderBy))
                        {
                            result = result.AsEnumerable().ApplySort(queryParameters.OrderBy).ToList(); ;
                        }
                    }
                    catch (Exception e)
                    {
                    }
                }
            }
            if (string.IsNullOrEmpty(queryParameters.SearchPropertyName) && !string.IsNullOrEmpty(queryParameters.OrderBy))
            {
                result = await query.ApplySort(queryParameters.OrderBy).ToListAsync();
            }
            return result;

        }

        public static IQueryable<TEntity> ApplySort<TEntity>(this IQueryable<TEntity> entities, string orderByQueryString) where TEntity : class
        {
            if (!entities.Any() || string.IsNullOrWhiteSpace(orderByQueryString))
                return entities;
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

        public static IQueryable<TEntity> ApplySort<TEntity>(this IEnumerable<TEntity> entities, string orderByQueryString) where TEntity : class
        {
            if (!entities.Any() || string.IsNullOrWhiteSpace(orderByQueryString))
                return entities.AsQueryable();
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
            return entities.AsQueryable().OrderBy(orderQuery).AsQueryable();
        }
    }
}
