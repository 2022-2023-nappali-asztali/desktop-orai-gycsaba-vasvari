﻿using KretaCommandLine.QueryParameter;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection;

namespace KretaWebApi.Repos.Base
{

    // https://daedtech.com/linq-order-by-when-you-have-property-name/

    public static class DbSetExtension
    {
        public static IQueryable<TEntity>? SearchById<TEntity>(this DbSet<TEntity> dbSet, string propertyName, long id) where TEntity : class
        {
            if (dbSet.Any() && !string.IsNullOrEmpty(propertyName))
            {
                PropertyInfo? propertyInfo = dbSet.First().GetType().GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfo is object)
                {
                    try
                    {
                        var result = dbSet.Where(entity => (long) propertyInfo.GetValue(entity, null) == id);
                        return result;
                    }
                    catch (Exception e)
                    {
                    }
                }
            }
            return null;
        }

        public static IQueryable<TEntity>? Filtring<TEntity>(this DbSet<TEntity> dbSet, QueryParameters queryParameters) where TEntity : class
        {
            if (dbSet.Any() && !string.IsNullOrEmpty(queryParameters.SearchPropertyName))
            {
                PropertyInfo? propertyInfo = dbSet.First().GetType().GetProperty(queryParameters.SearchPropertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfo is object)
                {
                    try
                    {
                        var lowerCaseSearchTerm = queryParameters.SearchTerm.Trim().ToLower();
                        var result = dbSet.Where(entity => propertyInfo.GetValue(entity, null).ToString().ToLower().Contains(lowerCaseSearchTerm));
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
