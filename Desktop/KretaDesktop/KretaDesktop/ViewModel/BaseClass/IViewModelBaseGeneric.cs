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

        public void Insert(TEntity entiy);
        public void Insert(IList<TEntity> collection);
        public void Update(TEntity entity);
        public void Delete(TEntity entity);
        public void DeleteAll();

        public int GetIndex(TEntity entity);
    }
}
