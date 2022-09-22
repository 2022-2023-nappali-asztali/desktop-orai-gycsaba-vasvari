using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KretaCommandLine.Model.Interface.Base
{
    public interface IAddress : IId
    {
        public string City { get; set; }
        public string StreetAndNumber { get; set; }
        public int PostCode { get; set; }

        public string WritingAddresses
        {
            get => $"{this.City}\n{this.StreetAndNumber}\n{this.PostCode}";
        }
    }
}
