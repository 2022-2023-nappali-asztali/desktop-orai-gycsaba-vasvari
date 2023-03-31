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
        }
    }
}
