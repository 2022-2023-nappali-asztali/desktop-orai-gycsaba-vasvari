using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KretaCommandLine.Model.Abstract;
using KretaDesktop.Services;

namespace KretaDesktop.ViewModel.BaseClass
{
    public class ViewModelBase<TEntity, TCollection> : ServiceViewModelBase<TEntity>, IViewModelBase<TEntity, TCollection>
        where TEntity : ClassWithId, new()
        where TCollection : ICollection<TEntity>, new()
    {

        public TCollection Items { get; set; } = new();

        public bool HasItems => Items.Any();  //Items.Count > 0;
        public long NextId => HasItems ? Items.Select(entity => entity.Id).Max() + 1 : 1;

        public ViewModelBase(IAPIService service) : base(service)
        {
            Items = new();
        }

        protected virtual async void InitializePagedPage()
        {
            var result = await SelectAllRecordAsync();
            Insert(result);            
        }

        protected virtual async void RefreshPagedItems()
        {
            var result = await SelectAllRecordAsync();
            DeleteAll();
            Insert(result);
            
        }

        protected async Task Insert(TEntity entity)
        {
            await _service.Insert<TEntity>(entity);
            RefreshPagedItems();
        }

        protected async Task Delete(TEntity entity)
        {
            await _service.Delete<TEntity>(entity);
            RefreshPagedItems();
        }


        protected void Insert(IList<TEntity> collection)
        {
            foreach (var item in collection)
            {
                Items.Add(item);
            }
        }

        protected void Update(TEntity entity)
        {
            int index = GetIndex(entity);
            if (index != -1)
                Items[index] = (TEntity) entity.Clone();
        }

        protected void DeleteAll()
        {
            Items.Clear();
        }

        protected int GetIndex(TEntity entity)
        {
            return Items.IndexOf(Items.FirstOrDefault(e => e.Id == entity.Id));
        }

        protected int GetIndex(long id)
        {
            return Items.IndexOf(Items.FirstOrDefault(e => e.Id == id));
        }
    }
}
