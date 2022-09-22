using KretaCommandLine.Model.Interface.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KretaCommandLine.Model.Base
{
    public abstract class Person : IId, IEquatable<Person>, IComparable
    {
        public long Id { get; set;}
   
        private IPerson person;
        private IAddress address;
        private IAccount account;

        public bool Equals(Person other)
        {
            return false;
        }

        public int CompareTo(object obj)
        {
            return 0;
        }
    }
}
