using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace KretaCommandLine.APIModel
{
    public class PagedList<T>
    {
        public Collection<T> Items { get; set; }

        public int NumberOfPage { get; set; }
        public int NumberOfItems { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }

        public PagedList()
        {
            CurrentPage = 0;
            NumberOfPage = int.MaxValue;
            PageSize = 10;
            NumberOfItems = 0;
        }

        public PagedList(Collection<T>? items,int numberOfPage, int pageSize)
        {
            // Amikor lekérjük az első oldalt akkor megadjuk az aktuálissan megjelenítésre kerül oldalt
            // és hogy egy oldalon hány items lehet
            CurrentPage= numberOfPage;
            PageSize = pageSize;
            // Ha máv van elemünk eltároljuk őket            
            if (items!=null)
                Items = items;
            else
                Items = new Collection<T>();
        }

    }
}
