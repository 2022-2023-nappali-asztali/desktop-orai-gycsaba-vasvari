using APIHelpersLibrary.Paged;
using KretaCommandLine.API;
using KretaCommandLine.Model.Abstract;
using KretaDesktop.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KretaDesktop.ViewModel.BaseClass
{
    public abstract class ServiceViewModelBase<TEntity> : ViewModelBase where TEntity : ClassWithId, new()
    {
        protected IAPIService _service;

        public ServiceViewModelBase(IAPIService service)
        {
            _service = service;
        }

        protected async ValueTask<PagingResponse<TEntity>> GetPaged(ItemParameters parameters)
        {
            return await _service.GetPageAsync<TEntity>(parameters);
        }

        public async ValueTask<PagingResponse<TEntity>> SelectAllIncludedRecordPagedAsync(ItemParameters parameters)
        {
            return await _service.SelectAllIncludedRecordPagedAsync<TEntity>(parameters);
        }

        protected async ValueTask<List<TEntity>> SelectAllRecordAsync()
        {
            return await _service.SelectAllRecordAsync<TEntity>();
        }


        protected async ValueTask<List<TEntity>> SelectAllIncludedRecordAsync()
        {
            return await _service.SelectAllIncludedRecordAsync<TEntity>();
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
