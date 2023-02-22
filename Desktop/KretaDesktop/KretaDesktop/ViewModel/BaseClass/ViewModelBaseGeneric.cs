using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using KretaCommandLine.Model;
using KretaCommandLine.Model.Abstract;

namespace KretaDesktop.ViewModel.BaseClass
{
    public class ViewModelBase<TEntity, TCollection> :ViewModelBase, IViewModelBase<TEntity, TCollection>
        where TEntity : ClassWithId, new()
        where TCollection : Collection<TEntity>, new()
    {

        public TCollection Items { get; set; } = new();

        public bool HasItems => Items.Any();  //Items.Count > 0;

        public long NextId => Items.Select(entity => entity.Id).Max() + 1;

        public void Insert(TEntity entiy)
        {
            entiy.Id = NextId;
            Items.Add(entiy);
        }

        public void Delete(TEntity entity)
        {
            Items.Remove(entity);
        }

        public void Insert(IList<TEntity> collection)
        {
            foreach (var item in collection)
            {
                Items.Add(item);
            }
        }

        public void Update(TEntity entity)
        {
            int index = GetIndex(entity);
            if (index != -1)
                Items[index] = (TEntity) entity.Clone();
        }

        public void DeleteAll()
        {
        }

        public int GetIndex(TEntity entity)
        {
            return Items.IndexOf(Items.FirstOrDefault(e => e.Id == entity.Id));
        }

        public int GetIndex(long id)
        {
            return Items.IndexOf(Items.FirstOrDefault(e => e.Id == id));
        }
    }
}
