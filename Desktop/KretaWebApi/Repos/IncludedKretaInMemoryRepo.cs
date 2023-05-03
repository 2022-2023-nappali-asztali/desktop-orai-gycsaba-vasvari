using APIHelpersLibrary.Paged;
using KretaCommandLine.Model.Abstract;
using KretaCommandLine.QueryParameter;
using KretaWebApi.Contexts;
using KretaWebApi.Repos.Base;
using Microsoft.EntityFrameworkCore;

namespace KretaWebApi.Repos
{
    public class IncludedKretaInMemoryRepo : IncludedRepoBase<InMemoryContext>
    {
        public IncludedKretaInMemoryRepo(IDbContextFactory<InMemoryContext> dbContextFactory) : base( dbContextFactory)
        {

        }

        protected override IQueryable<TEntity>? GetAllIncluded<TEntity>() where TEntity : class
        {
            return null;
        }
    }
}
