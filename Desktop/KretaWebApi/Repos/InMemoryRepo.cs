using EF.Contexts;
using KretaCommandLine.Model.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KretaWebApi.Repos
{
    public class InMemoryRepo<TEntity> : RepoBase<TEntity> where TEntity : ClassWithId
    {
        public InMemoryRepo(InMemoryContext dbContext) : base(dbContext)
        {
        }
    }
}

        

