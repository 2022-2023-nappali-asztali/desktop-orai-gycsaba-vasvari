using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace KretaCommandLine.APIModel
{
    public class PagedList<T> : Collection<T>
    {
        private QueryStringParameters _queryParameters;
        public QueryStringParameters QueryParameters 
        { 
            get { return _queryParameters; } 
            set { _queryParameters = value; } 
        }

        public PagedList()
        {
            _queryParameters.CurrentPage = 0;
            _queryParameters.NumberOfPage = int.MaxValue;
            _queryParameters.PageSize = 10;
            _queryParameters.NumberOfItems = 0;
        }

        public PagedList(List<T> items,int numberOfPage, int pageSize)
        {
            // Amikor lekérjük az első oldalt akkor megadjuk az aktuálissan megjelenítésre kerül oldalt
            // és hogy egy oldalon hány items lehet
            _queryParameters.CurrentPage= numberOfPage;
            _queryParameters.PageSize = pageSize;
            // Ha máv van elemünk eltároljuk őket
            
        }

    }
}
