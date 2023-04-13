﻿using KretaCommandLine.QueryParameter;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection;

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
                        var lowerCaseSearchTerm = queryParameters.SearchTerm.Trim().ToLower();
                        result = list.Where(entity => propertyInfo.GetValue(entity, null).ToString().ToLower().Contains(lowerCaseSearchTerm)).ToList();
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
