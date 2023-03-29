using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KretaDesktop.ViewModel.BaseClass
{
    public interface IListViewModelBase<TEntity>
    {
        public TEntity SelectedItem { get; set; }
        public RelayCommand NewCommand { get; set; }
        public RelayCommand RemoveCommand { get; set; }
        public RelayCommand SaveAndRefreshCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand ClearFormCommand { get; set; }
        public RelayCommand RemoveAllCommand { get; set; }
    }
}
