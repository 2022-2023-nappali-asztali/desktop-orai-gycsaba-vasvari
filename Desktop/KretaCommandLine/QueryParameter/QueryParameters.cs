using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KretaCommandLine.QueryParameter
{
    public class QueryParameters
    {
        public string SearchedPropertyName { get; set; }
        public string SearchTerm { get; set; }
        public string SortedTerm { get; set; }

        public bool IsValid => SearchTerm!= null || SortedTerm!= null;
    }
}
