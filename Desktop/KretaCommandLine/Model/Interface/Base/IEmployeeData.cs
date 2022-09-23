using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KretaCommandLine.Model.Interface.Base
{
    public interface IEmployeeData
    {
        public ulong TaxNumber { set; get; }
        public ulong Salary { set; get; }
        public int NumberOfChildren { set; get }
    }
}
