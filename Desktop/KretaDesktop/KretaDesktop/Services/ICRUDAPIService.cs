using KretaCommandLine.APIModel;
using KretaCommandLine.Model.Abstract;
using System.Threading.Tasks;

namespace KretaDesktop.Services
{
    public interface ICRUDAPIService
    {
        Task Delete<TEntity>(ClassWithId entity);
        public Task<PagedList<TEntity>> GetPageAsync<TEntity>(QueryStringParameters queryString);
    }
}
