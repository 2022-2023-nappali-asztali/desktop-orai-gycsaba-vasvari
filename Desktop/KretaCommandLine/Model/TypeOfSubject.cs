using KretaCommandLine.Model.Abstract;

namespace KretaCommandLine.Model
{
    public class TypeOfSubject : ClassWithId
    {
		public string Name {get ;set; }

		public TypeOfSubject():base(-1)
		{
			Name = string.Empty;
		}

		public TypeOfSubject(long id, string name) : base(id)
		{
			this.Name = name;			
		}

        public override object Clone()
        {
			return new TypeOfSubject
			{
				Id = Id,
				Name = Name
			};
        }
    }
}
