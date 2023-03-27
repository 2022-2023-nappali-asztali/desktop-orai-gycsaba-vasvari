using EF.Contexts;
using Microsoft.EntityFrameworkCore;

namespace KretaWebApi.Extension
{
    public static class InMemoryContextExtension
    {
        public static void ConfigureInMemoryContext(this IServiceCollection services)
        {            
            services.AddDbContext<InMemoryContext>(
             
                options => options.UseInMemoryDatabase(databaseName: "KretaTest" + Guid.NewGuid().ToString())

            );
            services.AddDbContextFactory<InMemoryContext>();
        }
    }
}
