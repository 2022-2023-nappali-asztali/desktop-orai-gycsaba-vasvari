using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KretaCommandLine.Model.Interface.Base
{
    interface IPerson
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public bool Wooman { get; set; }

        public DateTime DataOfBirth { get; set; }
    }
}
