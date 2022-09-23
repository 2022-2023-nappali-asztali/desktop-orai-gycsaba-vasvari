using KretaCommandLine.Model.Interface.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KretaCommandLine.Model
{
    public class Address : IAddress
    {
        public long Id { get; set; }
        public string City { get; set; }
        public string StreetAndNumber { get; set ; }
        public int PostCode { get; set; }

        public Address(long id,string city, string streetAndNumber, int postCode)
        {
            City = city;
            StreetAndNumber = streetAndNumber;
            PostCode = postCode;
            Id = id;
        }

        public Address()
        {
            City = string.Empty;
            StreetAndNumber = string.Empty;
            PostCode = -1;
            Id = -1;
        }
    }
}
