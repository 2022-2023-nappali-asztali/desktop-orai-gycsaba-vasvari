using KretaCommandLine.Model.Interface.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KretaCommandLine.Model.Base
{
    public abstract class AbstractPerson : IId, IPerson, IEquatable<AbstractPerson>, IComparable
    {
        public long Id { get; set;}
   
        private IAddress address;
        private IAccount account;

    }
}
