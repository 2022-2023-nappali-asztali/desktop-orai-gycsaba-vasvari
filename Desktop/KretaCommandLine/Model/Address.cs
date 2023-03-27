using KretaCommandLine.Model.Abstract;
using KretaCommandLine.Model.Interface.Base;

namespace KretaCommandLine.Model
{
    public class Address : ClassWithId
    {
        public string City { get; set; }
        public string StreetAndNumber { get; set ; }
        public int PostCode { get; set; }

        public Address(long id,string city, string streetAndNumber, int postCode) : base(id)
        {
            City = city;
            StreetAndNumber = streetAndNumber;
            PostCode = postCode;
            Id = id;
        }

        public Address() : base(-1)
        {
            City = string.Empty;
            StreetAndNumber = string.Empty;
            PostCode = -1;
            Id = -1;
        }

        public override object Clone()
        {
            return new Address();
        }
    }
}
