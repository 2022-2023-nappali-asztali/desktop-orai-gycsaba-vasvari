using APIHelpersLibrary.Paged;
using KretaCommandLine.Model.Abstract;
using KretaCommandLine.QueryParameter;
using KretaWebApi.Contexts;
using KretaWebApi.Repos.Base;
using Microsoft.EntityFrameworkCore;

namespace KretaWebApi.Repos
{
    public class ClassWithIdKretaInMemeoryRepo : ClassWithIdRepoBase<InMemoryContext>
    {
        public ClassWithIdKretaInMemeoryRepo(IDbContextFactory<InMemoryContext> dbContextFactory) : base( dbContextFactory)
        {

        }
    }
}
