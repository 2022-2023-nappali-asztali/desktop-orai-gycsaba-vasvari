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
        where TCollection : Collection<TEntity>, new()
    {

        public TCollection Items { get; set; } = new();    

        public void Add(TEntity entiy)
        {
            Items.Add(entiy);
        }

        public void Delete(TEntity entity)
        {
            Items.Remove(entity);
        }

        public void Add(IList<TEntity> collection)
        {
            foreach (var item in collection)
            {
                Items.Add(item);
            }
        }
    }
}
