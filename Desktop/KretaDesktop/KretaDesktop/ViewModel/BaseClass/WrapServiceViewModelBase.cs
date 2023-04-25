using KretaCommandLine.DTO;
using KretaCommandLine.Model;
using KretaDesktop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KretaDesktop.ViewModel.BaseClass
{
    public class WrapServiceViewModelBase : InitializedViewModelBase
    {
        protected IWrapService _service;

        public WrapServiceViewModelBase(IWrapService service)
        {
            _service = service;
        }

        public override Task OnInitialize()
        {
            return Task.CompletedTask;
        }
    }
}
