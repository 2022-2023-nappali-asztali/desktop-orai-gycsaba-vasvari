using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using KretaCommandLine.Model.Abstract;
using KretaDesktop.Services;
using KretaDesktop.ViewModel.BaseClass.Interface;

namespace KretaDesktop.ViewModel.BaseClass
{
    public abstract class ViewModelBase<TEntity, TCollection> : ServiceViewModelBase<TEntity>, IViewModelBase<TEntity, TCollection>
        where TEntity : ClassWithId, new()
        where TCollection : ObservableCollection<TEntity>, new()
    {
        protected bool _withIncludedData = false;

        public TCollection Items { get; set; } = new();

        protected bool HasItems => Items.Any();  //Items.Count > 0;
        protected long NextId => HasItems ? Items.Select(entity => entity.Id).Max() + 1 : 1;

        public ViewModelBase(IAPIService service) : base(service)
        {
            Items = new();
        }

        public abstract Task OnInitialize();

        protected virtual async Task InitializeItems()
        {
            await RefreshItems();
        }

        protected virtual async Task InitializeWithIncludedData()
        {
            _withIncludedData= true;
            await RefreshItems();
        }

        protected virtual async Task RefreshItems()
        {
            List<TEntity> result = null;
            if (_withIncludedData)
            {
                result=await SelectAllIncludedRecordAsync();
            }
            else
            {
                result = await SelectAllRecordAsync();
            }
            if (result != null)
            {
                DeleteAllItems();
                AddToItems(result);
            }
        }

        protected async Task SaveRecord(TEntity entity)
        {
            await _service.Save<TEntity>(entity);
            await RefreshItems();
        }

        protected async Task DeleteRecord(TEntity entity)
        {
            await _service.Delete<TEntity>(entity.Id);
            await RefreshItems();
        }

        protected async Task InsertRecord(TEntity entity)
        {
            await _service.Save<TEntity>(entity);
            await RefreshItems();
        }

        protected async Task UpdateRecord(TEntity entity)
        {
            await _service.Save<TEntity>(entity);
            await RefreshItems();
        }

        protected void AddToItems(IList<TEntity> collection)
        {
            if (collection is object)
            {
                foreach (var item in collection)
                {
                    Items.Add(item);
                }
            }
        }

        protected void DeleteAllItems()
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
