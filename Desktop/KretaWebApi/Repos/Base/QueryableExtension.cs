using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
using KretaCommandLine.QueryParameter;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using KretaCommandLine.Model;

namespace KretaWebApi.Repos.Base
{
    public static class QueryableExtension
    {
        public static IQueryable<TEntity>? SearchById<TEntity>(this IQueryable<TEntity> query, string propertyName, long id) where TEntity : class
        {
            if (query.Any() && !string.IsNullOrEmpty(propertyName))
            {
                PropertyInfo? propertyInfo = query.First().GetType().GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfo is object)
                {
                    try
                    {
                        var result = query.Where(entity => (long)propertyInfo.GetValue(entity, null) == id);
                        return result;
                    }
                    catch (Exception e)
                    {
                    }
                }
            }
            return null;
        }


        public static IQueryable<TEntity>? Filtring<TEntity>(this IQueryable<TEntity> query, QueryParameters queryParameters) where TEntity : Subject
        {
            if (query.Any() && !string.IsNullOrEmpty(queryParameters.SearchPropertyName))
            {
                PropertyInfo? propertyInfo = query.First().GetType().GetProperty(queryParameters.SearchPropertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfo is object)
                {
                    try
                    {
                        var lowerCaseSearchTerm = queryParameters.SearchTerm.Trim().ToLower();
                        //var result = query.Where(entity => propertyInfo.GetValue(entity, null).ToString().ToLower().Contains(lowerCaseSearchTerm));
                        var result = query.Where(entity => entity.SubjectName.ToString().ToLower().Contains(lowerCaseSearchTerm));
                        var proba=result.ToList();
                        return result;
                    }
                    catch (Exception e)
                    {
                    }
                }
            }
            return null;
        }

        public static IQueryable<TEntity>? FiltringAndSorting<TEntity>(this IQueryable<TEntity> query, QueryParameters queryParameters) where TEntity : class
        {
            IQueryable<TEntity>? result = null;
            if (!string.IsNullOrEmpty(queryParameters.SearchTerm) && !string.IsNullOrEmpty(queryParameters.SearchPropertyName))
            {
               result=query.Filtring<TEntity>(queryParameters);
            }
            else
                result= query;
            if (!string.IsNullOrEmpty(queryParameters.OrderBy))
            {
                if (result is object)
                {
                    List<TEntity> list = result.ToList();
                    IQueryable<TEntity> sorted = result.ApplySort<TEntity>(queryParameters.OrderBy);
                    return sorted;
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

    }
}
