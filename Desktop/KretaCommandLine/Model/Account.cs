using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KretaCommandLine.Model.Interface.Base;

namespace KretaCommandLine.Model
{
    public class Account : IAccount,IEquatable<object>
    {
        public long Id { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }

        public Account(long id,string loginName, string password )
        {
            LoginName = loginName;
            Password = password;
            Id = id;
        }

        public Account()
        {
            LoginName = string.Empty;
            Password = string.Empty;
            Id = -1;
        }

        public bool Equals(object other)
        {
            throw new NotImplementedException();
        }
    }
}
