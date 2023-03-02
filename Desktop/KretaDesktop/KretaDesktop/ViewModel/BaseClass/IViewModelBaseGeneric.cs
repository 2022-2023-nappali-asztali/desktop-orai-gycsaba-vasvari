using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KretaDesktop.ViewModel.BaseClass
{
    public interface IViewModelBase<TEntity, TCollection>
    {
        public TCollection Items { get; set; }

   }
}
