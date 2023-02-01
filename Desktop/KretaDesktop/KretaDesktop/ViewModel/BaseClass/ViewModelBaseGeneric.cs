using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using KretaCommandLine.Model;

namespace KretaDesktop.ViewModel.BaseClass
{
    public class ViewModelBase<TEntity, TCollection> :ViewModelBase
        where TEntity : class
        where TCollection : Collection<TEntity>
    {

        protected TCollection _collection;       

        public void Add(TEntity entiy)
        {
            _collection.Add(entiy);
        }

        public void Add(IList<TEntity> collection)
        {
            foreach (var item in collection)
            {
                _collection.Add(item);
            }
        }
    }
}
