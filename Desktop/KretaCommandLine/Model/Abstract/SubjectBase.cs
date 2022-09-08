using System;

namespace KretaCommandLine.Model.Abstract
{
    public abstract class SubjectBase : ClassWithId
    {
        public virtual string SubjectName { get; set; }

        public SubjectBase(long id, string subName)
        {
            this.Id = id;
            this.SubjectName = subName;
        }

        public SubjectBase()
        {
            this.Id = -1;
            this.SubjectName = String.Empty;
        }

        public override string ToString()
        {
            return Id + ". " + SubjectName;
        }
    }
}
