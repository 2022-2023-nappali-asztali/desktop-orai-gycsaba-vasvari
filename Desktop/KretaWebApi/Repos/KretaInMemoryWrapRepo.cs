using KretaCommandLine.Model;
using KretaWebApi.Contexts;
using KretaWebApi.Repos.Base;
using Microsoft.EntityFrameworkCore;

namespace KretaWebApi.Repos
{
    public class KretaInMemoryWrapRepo : WrapRepoBase<InMemoryContext>, IWrapRepoBase
    {
        public KretaInMemoryWrapRepo(IDbContextFactory<InMemoryContext> dbContextFactory) : base(dbContextFactory)
        {
        }
    }
}
