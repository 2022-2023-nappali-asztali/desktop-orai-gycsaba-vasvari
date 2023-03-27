using KretaWebApi.Contexts;
using KretaWebApi.Repos;
using Microsoft.EntityFrameworkCore;

namespace KretaWebApi.Extension
{
    public class KretaWebApiRepo : RepoBase<InMemoryContext>
    {
        public KretaWebApiRepo(IConfiguration configuration, IDbContextFactory<InMemoryContext> dbContextFactory) : base(configuration, dbContextFactory)
        {
        }
    }
}
