using Microsoft.EntityFrameworkCore;

namespace KretaWebApi.Contexts
{
    public static class InMemoryContextOptions
    {
        public static DbContextOptions<KretaContext> contextOptions = new DbContextOptionsBuilder<KretaContext>()
            .UseInMemoryDatabase(databaseName: "KretaTest" + Guid.NewGuid().ToString())
            .Options;
    }
}
