using KretaCommandLine.Model.Abstract;
using KretaWebApi.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KretaWebApi.Repos
{
    public class MySqlRepo<TEntity> : RepoBase<TEntity> where TEntity : ClassWithId
    {
     
        public MySqlRepo(MySqlContext dbContext) : base(dbContext)
        {
        }
    }
}
