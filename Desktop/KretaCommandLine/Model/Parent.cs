using KretaCommandLine.Model;
using KretaCommandLine.Model.Abstract;

namespace KretaCommandLine.Model
{
    public class Parent : ClassWithId
    {
        public string Name { get ; set ; }
        public bool IsWoman { get ; set ; }

        // navigation property
        // one-one
        public int ParentAddressId { get; set; }
        public virtual Address ParentAddress { get; set; }

        public Parent(long id, string name, bool isWoman)
            : base(id)
        {
            Name = name;
            IsWoman = isWoman;
        }

        public Parent()
            : base(-1)
        {
            Name = string.Empty;
            IsWoman = false;
        }

        public override object Clone()
        {
            return new Parent
            {
                Id = Id,
                Name = Name,
            };
        }
    }
}
