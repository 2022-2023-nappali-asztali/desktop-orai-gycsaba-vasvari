using KretaCommandLine.Model;
using KretaWebApi.Contexts;
using KretaWebApi.Repos.Base;
using Microsoft.EntityFrameworkCore;

namespace KretaWebApi.Repos
{
    public class SchoolClassInMemoryRepo : SchoolClassRepoBase<InMemoryContext>, ISchoolClassRepoBase
    {
        public SchoolClassInMemoryRepo(IDbContextFactory<InMemoryContext> dbContextFactory) : base(dbContextFactory)
        {
        }
    }
}
