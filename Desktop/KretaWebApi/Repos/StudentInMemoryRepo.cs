using KretaWebApi.Contexts;
using KretaWebApi.Repos.Base;
using Microsoft.EntityFrameworkCore;

namespace KretaWebApi.Repos
{
    public class StudentInMemoryRepo : StudentRepoBase<InMemoryContext>, IStudentRepoBase
    {
        public StudentInMemoryRepo(IDbContextFactory<InMemoryContext> dbContextFactory) : base(dbContextFactory)
        {
        }
    }
}
