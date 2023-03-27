﻿using KretaCommandLine.Model;
using KretaWebApi.Repos;

namespace KretaWebApi.Extension
{
    public static class KretaWebApiExtensions
    {
        public static void ConfigureRepo(this IServiceCollection services)
        {
            services.AddScoped<IRepoBase, KretaWebApiRepo>();
        }
    }
}