using KretaCommandLine.Model.Abstract;

namespace KretaCommandLine.Model
{
    public class Settings : ClassWithId
    {
        private int Id;
        private int NumberOfItems;

        public Settings(int id, int numberOfItems) :base (id)
        {
            NumberOfItems = numberOfItems;
        }

        public Settings() : base(-1)
        {
            NumberOfItems = -1;
        }

        public override object Clone()
        {
            return new Settings();
        }
    }
}
