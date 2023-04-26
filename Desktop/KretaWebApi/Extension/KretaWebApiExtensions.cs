using KretaCommandLine.Model;
using KretaWebApi.Contexts;
using KretaWebApi.Repos;
using KretaWebApi.Repos.Base;

namespace KretaWebApi.Extension
{
    public static class KretaWebApiExtensions
    {
        public static void ConfigureRepo(this IServiceCollection services)
        {
            services.AddScoped<IRepoBase, KretaInMemoryRepo>();
            services.AddScoped<IStudentRepoBase, StudentInMemoryRepo>();
            services.AddScoped<ISchoolClassRepoBase, SchoolClassInMemoryRepo>();
            services.AddScoped<IWrapRepoBase, KretaInMemoryWrapRepo>();
        }

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithExposedHeaders("X-Pagination")
                    );
            });
        }
    }
}
