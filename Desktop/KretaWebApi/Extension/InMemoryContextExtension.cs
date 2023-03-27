using KretaWebApi.Contexts;
using Microsoft.EntityFrameworkCore;

namespace KretaWebApi.Extension
{
    public static class InMemoryContextExtension
    {
        public static void ConfigureInMemoryContext(this IServiceCollection services)
        {                        
            services.AddDbContextFactory<KretaContext>(
                options => options.UseInMemoryDatabase(databaseName: "KretaTest" + Guid.NewGuid().ToString())
                );
        }
    }
}
