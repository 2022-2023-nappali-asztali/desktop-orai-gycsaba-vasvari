using KretaWebApi.Contexts;
using KretaWebApi.Repos;
using Microsoft.EntityFrameworkCore;

namespace KretaWebApi.Extension
{
    public class KretaWebApiRepo : RepoBase<KretaContext>
    {
        public KretaWebApiRepo(IConfiguration configuration, IDbContextFactory<KretaContext> dbContextFactory) : base(configuration, dbContextFactory)
        {
        }
    }
}
