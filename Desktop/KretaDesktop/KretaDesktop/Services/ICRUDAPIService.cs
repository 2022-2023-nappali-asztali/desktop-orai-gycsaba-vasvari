using KretaCommandLine.APIModel;
using System.Threading.Tasks;

namespace KretaDesktop.Services
{
    public interface ICRUDAPIService
    {
        public Task<PagedList<TEntity>> GetPageAsync<TEntity>(QueryStringParameters queryString);
    }
}
