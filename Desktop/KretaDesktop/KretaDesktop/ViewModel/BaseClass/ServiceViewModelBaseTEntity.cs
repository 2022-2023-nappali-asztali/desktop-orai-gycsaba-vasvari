using APIHelpersLibrary.Paged;
using KretaCommandLine.API;
using KretaCommandLine.Model.Abstract;
using KretaCommandLine.QueryParameter;
using KretaDesktop.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KretaDesktop.ViewModel.BaseClass
{
    public abstract class ServiceViewModelBase<TEntity> : InitializedViewModelBase where TEntity : ClassWithId, new()
    {
        protected IAPIService _service;

        public ServiceViewModelBase(IAPIService service)
        {
            _service = service;
        }

        protected async ValueTask<PagingResponse<TEntity>> GetPaged(PagingParameters itemParameters, QueryParameters queryParameter)
        {
            return await _service.GetPageAsync<TEntity>(itemParameters,queryParameter);
        }

        public async ValueTask<PagingResponse<TEntity>> SelectAllIncludedRecordPagedAsync(PagingParameters itemParameters, QueryParameters queryParameter)
        {
            return await _service.SelectAllIncludedRecordPagedAsync<TEntity>(itemParameters, queryParameter);
        }

        protected async ValueTask<List<TEntity>> SelectAllRecordAsync(QueryParameters queryParameter)
        {
            return await _service.SelectAllRecordAsync<TEntity>(queryParameter);
        }


        protected async ValueTask<List<TEntity>> SelectAllIncludedRecordAsync(QueryParameters queryParameter)
        {
            return await _service.SelectAllIncludedRecordAsync<TEntity>(queryParameter);
        }

        protected async ValueTask<TEntity> GetBy(long id)
        {
            return await _service.GetBy<TEntity>(id);
        }

        protected async ValueTask<APICallState> Save(TEntity item)
        {
            return await _service.Save(item);
        }

        protected async ValueTask<APICallState> Delete(long id)
        {
            return await _service.Delete<TEntity>(id);
        }
    }
}
