using KretaCommandLine.Model;
using KretaWebApi.Contexts;
using Microsoft.EntityFrameworkCore;

namespace KretaWebApi.Extension
{
    public static class InMemoryContextExtension
    {
        public static void ConfigureInMemoryContext(this IServiceCollection services)
        {
            string dbName = "KretaTest" + Guid.NewGuid();
            services.AddDbContextFactory<KretaContext>(
                options => options.UseInMemoryDatabase(databaseName: dbName)
                );
            services.AddDbContextFactory<InMemoryContext>(
                options => options.UseInMemoryDatabase(databaseName: dbName)
                );

        }
    }
}
