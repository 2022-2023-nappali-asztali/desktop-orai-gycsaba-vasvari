﻿using KretaCommandLine.Model.Abstract;
using KretaDesktop.Services;
using KretaDesktop.ViewModel.BaseClass.Interface;
using KretaDesktop.ViewModel.Command;
using System;
using System.Threading.Tasks;

namespace KretaDesktop.ViewModel.BaseClass
{
    public class CRUDListViewModel<TEntity> : SearchAndSortViewModel<TEntity>, ICRUDListViewModelBase<TEntity>
        where TEntity : ClassWithId, new()
    {
        public CRUDListViewModel(IAPIService service) : base(service)
        {
            RemoveCommand = new AsyncRelayCommandWithParameter(parameter => Remove(parameter), (ex) => OnException());
            SaveAndRefreshCommand = new AsyncRelayCommandWithParameter(parameter => SaveAndRefresh(parameter), (ex) => OnException());
            NewCommand = new RelayCommand(execute => New());
            CancelCommand = new RelayCommand(execute => Cancel());
            ClearFormCommand = new RelayCommand(execute => Clear());

            IsCRUDVisible = true;
        }

        public RelayCommand NewCommand { get; set; }
        public AsyncRelayCommandWithParameter RemoveCommand { get; set; }
        public AsyncRelayCommandWithParameter SaveAndRefreshCommand { get; set; }
        public RelayCommand ClearFormCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand RemoveAllCommand { get; set; }

        protected async Task Remove(object parameter)
        {
            if (parameter is TEntity entity)
            {
                await DeleteRecord(entity);
                if (HasItems)
                    SelectFirstRow();
                else
                    DisplayedItem = new TEntity();
            }
        }

        protected async Task SaveAndRefresh(object parameter)
        {
            if (DisplayedItem is object)
            {
                await SaveRecord(DisplayedItem);
                IsNewMode = false;
                SelectRowContains(DisplayedItem);
            }
        }

        protected void New()
        {            
            DisplayedItem = new TEntity();
            IsNewMode = true;
        }

        protected void Cancel()
        {
            IsNewMode = false;
            SelectedItemIndex = -1;
            SelectRowContains(SelectedItemId);
        }

        protected void Clear()
        {
            DisplayedItem = new TEntity();
        }

        private void OnException()
        {
        }

    }
}
