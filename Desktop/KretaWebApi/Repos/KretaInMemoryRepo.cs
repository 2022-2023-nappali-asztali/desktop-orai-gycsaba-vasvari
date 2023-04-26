using APIHelpersLibrary.Paged;
using KretaCommandLine.Model.Abstract;
using KretaCommandLine.QueryParameter;
using KretaWebApi.Contexts;
using KretaWebApi.Repos.Base;
using Microsoft.EntityFrameworkCore;

namespace KretaWebApi.Repos
{
    public class KretaInMemoryRepo : RepoBase<InMemoryContext>
    {
        public KretaInMemoryRepo(IDbContextFactory<InMemoryContext> dbContextFactory) : base( dbContextFactory)
        {

        }
    }
}
