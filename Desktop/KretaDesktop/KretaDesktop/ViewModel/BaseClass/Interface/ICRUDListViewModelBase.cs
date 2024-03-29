﻿using KretaDesktop.ViewModel.Command;

namespace KretaDesktop.ViewModel.BaseClass.Interface
{
    public interface ICRUDListViewModelBase<TEntity>
    {
        public RelayCommand NewCommand { get; set; }
        public AsyncRelayCommandWithParameter RemoveCommand { get; set; }
        public AsyncRelayCommandWithParameter SaveAndRefreshCommand { get; set; }
        public AsyncRelayCommand SearchItemsCommand { get; set; }
        public RelayCommand ClearFormCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand RemoveAllCommand { get; set; }
    }
}
