using KretaCommandLine.Model.Abstract;
using System.Collections.Generic;

namespace KretaCommandLine.Model
{
    public class Teacher : ClassWithId
    {
        public string Name { get; set ; }
        public bool IsHeadTeacher { get ; set ; }
        public bool IsWoman { get ; set ; }

        // navigation property        
        // one - one
        public long TeacherAddressId { get; set; }
        public virtual Address TeacherAddress { get; set; }

        // many - many
        public virtual ICollection<Subject> TeachedSubjects { get; set; }


        public Teacher(long id, string name, bool isHeadTeacher, bool isWoman, long addressId)
            : base(id)
        {
            this.Name = name;
            this.IsHeadTeacher = isHeadTeacher;
            this.IsWoman = isWoman;
            this.TeacherAddressId = addressId;
        }

        public Teacher()
            : base(-1)
        {
            Name = string.Empty;
            IsHeadTeacher = false;
            IsWoman = false;
        }

        public override object Clone()
        {
            return new Teacher
            {
                Id = this.Id,
                Name = this.Name,
                IsWoman = this.IsWoman,
                IsHeadTeacher = this.IsHeadTeacher
            };
        }
    }
}
