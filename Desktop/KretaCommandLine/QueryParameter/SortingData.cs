using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KretaCommandLine.QueryParameter
{
    public class SortingData
    {
        public string SortingLabel;
        public string SortingDescription;
        public string SortingCommand;

        public SortingData()
        {
            SortingLabel = string.Empty;
            SortingDescription = string.Empty;
            SortingCommand = string.Empty;
        }

        public SortingData(string sortingLabel, string sortingDescription, string sortingCommand)
        {
            SortingLabel = sortingLabel;
            SortingDescription = sortingDescription;
            SortingCommand = sortingCommand;
        }
    }
}
