using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KretaCommandLine.Model.Interface.Base
{
    public interface IPerson :IId
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public bool Wooman { get; set; }

        public DateTime DataOfBirth { get; set; }

        public string HungarianFullName
        {
            get => $"{this.LastName} {this.FirstName}";
        }

        public string FullName
        {
            get => $"{this.FirstName} {this.LastName}";
        }

        public bool IsWoman
        {
            get => this.Wooman;
        }

        public bool IsMan
        {
            get => !this.Wooman;
        }

        public int Age
        {
            get
            {
                int age = 0;
                age = DateTime.Now.Year - DataOfBirth.Year;
                if (DateTime.Now.DayOfYear < DataOfBirth.DayOfYear)
                    age = age - 1;
                return age;
            }
        }

    }
}
