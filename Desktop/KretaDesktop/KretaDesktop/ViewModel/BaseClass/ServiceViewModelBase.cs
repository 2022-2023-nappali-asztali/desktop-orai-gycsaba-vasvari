using KretaCommandLine.Model.Abstract;
using KretaDesktop.Services;

namespace KretaDesktop.ViewModel.BaseClass
{
    public class ServiceViewModelBase<TEntity> : ViewModelBase where TEntity : ClassWithId, new()
    {
        protected ICRUDAPIService _service;

        public ServiceViewModelBase(ICRUDAPIService service)
        {
            _service = service;
        }
    }
}
