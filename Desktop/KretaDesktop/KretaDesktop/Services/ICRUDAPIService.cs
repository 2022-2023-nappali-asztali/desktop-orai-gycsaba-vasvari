using KretaCommandLine.APIModel;
using KretaCommandLine.Model.Abstract;
using System.Threading.Tasks;

namespace KretaDesktop.Services
{
    public interface ICRUDAPIService
    {
        public Task Delete<TEntity>(ClassWithId entity);
        public Task<PagedList<TEntity>> GetPageAsync<TEntity>(QueryStringParameters queryString);
        public Task Insert<TEntity>(ClassWithId entity);
    }
}
