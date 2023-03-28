using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using KretaCommandLine.Model.Abstract;
using KretaDesktop.Services;

namespace KretaDesktop.ViewModel.BaseClass
{
    public class ViewModelBase<TEntity, TCollection> :PagedListViewModelBase<TEntity>, IViewModelBase<TEntity, TCollection>
        where TEntity : ClassWithId, new()
        where TCollection : Collection<TEntity>, new()
    {

        public TCollection Items { get; set; } = new();

        public bool HasItems => Items.Any();  //Items.Count > 0;
        public long NextId => HasItems ? Items.Select(entity => entity.Id).Max() + 1 : 1;

        public ViewModelBase(IAPIService service) : base(service)
        {
        }

        protected async void InitializePagedPage()
        {
            SetPagedList(); // Van-e page vagy nincs, konfigurálhatósághoz
            await GetPageAsync();
            if (PagedList.Items.Any())
            {
                Insert(PagedList.Items);
            }
        }

        protected async override void RefreshPagedItems()
        {
            await GetPageAsync();
            if (PagedList!=null && PagedList.Items!=null && PagedList.Items.Any())
            {
                DeleteAll();
                Insert(PagedList.Items);
            }
        }

        protected async Task Insert(TEntity entity)
        {
            //entiy.Id = NextId;
            //Items.Add(entiy);
            await _service.Insert<TEntity>(entity);
            RefreshPagedItems();
        }

        protected async Task Delete(TEntity entity)
        {
            await _service.Delete<TEntity>(entity);
            RefreshPagedItems();
            //Items.Remove(entity);
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
