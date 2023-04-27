using KretaCommandLine.Model;
using Microsoft.EntityFrameworkCore;
using KretaWebApi.Extension;

namespace KretaWebApi.Contexts
{
   /* public static class MemoryContextOptionsSettings
    {
        private static DbContextOptions<InMemoryContext> MemoryContextOptions = new DbContextOptionsBuilder<InMemoryContext>()
           .UseInMemoryDatabase(databaseName: "KretaTest" + Guid.NewGuid().ToString())
           .Options;
    }*/
    public class KretaContext : DbContext
    {

        public DbSet<Subject>? Subject { get; set; }
        public DbSet<Teacher>? Teacher { get; set; }
        public DbSet<Student>? Student { get; set; }
        public DbSet<Address>? Address { get; set; }
        public DbSet<SchoolClass>? SchoolClass { get; set; }
        public DbSet<Parent>? Parent { get; set; }
        public DbSet<TypeOfSubject>? TypeOfSubject { get; set; }
        public DbSet<TeachTeacherSubject>? TeachTeacherSubject { get; set; }
        public DbSet<TeachTeacherSchoolClass>? TeachTeacherSchoolClass { get; set; }
        public DbSet<Settings>? Settings { get; set; }

        public KretaContext(DbContextOptions<KretaContext> options)
          : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // one - one relationship
            modelBuilder.Entity<Subject>()
                .HasOne<TypeOfSubject>(subject => subject.TypeOfSubject)
                .WithOne()
                .HasForeignKey<TypeOfSubject>(type => type.Id);


            modelBuilder.Entity<Teacher>()
                 .HasOne<Address>(teacher => teacher.TeacherAddress)
                 .WithOne()
                 .HasForeignKey<Teacher>(teacher => teacher.TeacherAddressId);

            modelBuilder.Entity<Student>()
                .HasOne<Address>(student => student.StudentAddress)
                .WithOne()
                .HasForeignKey<Student>(student => student.StudentAddressId);

            // one - many relationship
            modelBuilder.Entity<Student>()
                .HasOne<SchoolClass>(student => student.SchoolClassOfStudent)
                .WithMany(schoolClass => schoolClass.Students)
                .HasForeignKey(student => student.SchoolClassId);


            // many - many ralition ship
            modelBuilder.Entity<TeachTeacherSubject>()
                .HasKey(tts => new { tts.SubjectId, tts.TeacherId });
            modelBuilder.Entity<TeachTeacherSchoolClass>()
                .HasKey(tts => new { tts.TeacherId, tts.SchoolClassId});      


            modelBuilder.Entity<TeachTeacherSubject>()
                .HasOne<Teacher>(tts => tts.Techher)
                .WithMany()
                .HasForeignKey(tts => tts.TeacherId);

            modelBuilder.Entity<TeachTeacherSubject>()
                .HasOne<Subject>(tts => tts.Subject)
                .WithMany()
                .HasForeignKey(tts => tts.SubjectId);

        }
    }
}