using KretaDesktop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KretaDesktop.ViewModel.BaseClass
{
    public class ServiceViewModelBase : InitializedViewModelBase
    {
        protected IAPIService _service;

        public ServiceViewModelBase(IAPIService service)
        {
            _service = service;
        }

        public override Task OnInitialize()
        {
            return Task.CompletedTask;
        }
    }
}
