using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KretaCommandLine.Model.Interface.Base;

namespace KretaCommandLine.Model.Base
{
    public abstract class Employee : Person, IEmployeeData
    {
        public ulong TaxNumber { get; set; }
        public ulong Salary { get; set; }
        public int NumberOfChildren { get; set; }

        protected Employee(
            long id, 
            string firstName, 
            string lastName, 
            bool wooman, 
            DateTime dataOfBirth, 
            IAddress address, 
            IAccount account,
            ulong taxNumber, 
            ulong salary, 
            int numberOfChildren) : base(id, firstName,lastName,wooman,dataOfBirth,address,account)          

        {
            TaxNumber = taxNumber;
            Salary = salary;
            NumberOfChildren = numberOfChildren;
        }


        protected Employee(long id,string firstName,string lastName,bool wooman,DateTime dataOfBirth) 
            : base()

        {
            TaxNumber = ulong.MaxValue;
            Salary = ulong.MaxValue;
            NumberOfChildren = -1;
        }

        protected Employee(): base()
        {
            TaxNumber = ulong.MaxValue;
            Salary = ulong.MaxValue;
            NumberOfChildren = -1;
        }
    }
}
