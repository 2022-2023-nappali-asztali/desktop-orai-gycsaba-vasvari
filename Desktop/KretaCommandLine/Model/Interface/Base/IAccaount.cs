using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KretaCommandLine.Model.Interface.Base
{
    public interface IAccount : IId
    {
        public string LoginName { get; set; }

        public string Password { get; set; }

        public virtual bool IsLoginNameCorrect(string givenLoginName)
        {
            return false;
        }
        public virtual bool IsPasswordCorrect(string givenPassword)
        {
            return false;
        }
        public virtual bool VerifyLoginName(string givenLoginName)
        {
            return false;
        }
        public virtual bool VerifyPassword(string givenPassword)
        {
            return false;
        }

        public bool IsSuccessfulLogin(string givenLoginName, string givenPassword)
        {
            return this.IsLoginNameCorrect(givenLoginName) && this.IsPasswordCorrect(givenPassword);
        }

        public bool Equal(string loginName, string password)
        {
            return LoginName.Equals(loginName) && (Password.Equals(password));
        }
    }
}
