using KretaCommandLine.Model.Abstract;
using KretaDesktop.Services;

namespace KretaDesktop.ViewModel.BaseClass
{
    public class ServiceViewModelBase<TEntity> : ViewModelBase where TEntity : ClassWithId, new()
    {
        protected IAPIService _service;

        public ServiceViewModelBase(IAPIService service)
        {
            _service = service;
        }
    }
}
