using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KretaCommandLine.APIModel
{
    public class QueryStringParameters
    {
        public int NumberOfPage { get; set; }
        public int NumberOfItems { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }

        public bool HasPreviousPage => CurrentPage > 0;
        public bool HasNextPage => CurrentPage < NumberOfPage;
    }
}
