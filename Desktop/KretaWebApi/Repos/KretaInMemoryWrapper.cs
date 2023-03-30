using KretaWebApi.Contexts;
using KretaWebApi.Repos.Base;
using Microsoft.EntityFrameworkCore;

namespace KretaWebApi.Repos
{
    public class KretaInMemoryWrapper
    {
        private IStudentRepo? _studentRepo;
        public KretaInMemoryWrapper(IDbContextFactory<InMemoryContext> dbContextFactory, IStudentRepo studentRepo) 
        { 
            _studentRepo= studentRepo;
        }
    }
}
