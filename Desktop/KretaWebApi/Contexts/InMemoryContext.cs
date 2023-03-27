using KretaCommandLine.Model;
using Microsoft.EntityFrameworkCore;

namespace KretaWebApi.Contexts
{
    public class InMemoryContext : KretaContext
    {
        public InMemoryContext(DbContextOptions<KretaContext> options) : base(options)
        {
            MakteTestData();
        }


        private void MakteTestData()
        {
            if (Subject is object)
            {
                Subject.Add(new Subject(1, "Matematika", 1));
                Subject.Add(new Subject(2, "Magyar nyelv", 2));
                Subject.Add(new Subject(3, "Irodalom", 2));
                Subject.Add(new Subject(4, "Angol nyelv", 2));
                Subject.Add(new Subject(5, "Német nyelv", 2));
                Subject.Add(new Subject(6, "Történelem", 3));
                Subject.Add(new Subject(7, "Rajz", 4));
                Subject.Add(new Subject(8, "Testnevelés", 1));
                Subject.Add(new Subject(9, "Fizika", 1));
                Subject.Add(new Subject(10, "Kémia", 1));
                Subject.Add(new Subject(11, "Zene", 4));
                Subject.Add(new Subject(12, "Biológia", 2));
            }
            SaveChanges();
        }
    }
}
