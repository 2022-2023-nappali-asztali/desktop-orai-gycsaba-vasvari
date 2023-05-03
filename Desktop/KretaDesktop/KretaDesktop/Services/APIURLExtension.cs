using KretaCommandLine.Model.Abstract;
using System;

namespace KretaDesktop.Services
{
    public static class APIURLExtension
    {
        public static Uri GetHttpClientUri()
        {
            UriBuilder uri = new UriBuilder();
            uri = GetAPIUri(uri);
            return uri.Uri;
        }

        private static UriBuilder GetAPIUri(UriBuilder uri) 
        {
            uri.Scheme = "https";
            uri.Host = "localhost";
            uri.Port = 7555;
            return uri;
        }

        public static string SetRelativUrl<TEntity>() where TEntity : class, new()
        {
            return $"/api/{typeof(TEntity).Name}";
        }

        public static string SetRelativUrl() 
        {
            return $"/api";
        }
    }
}
