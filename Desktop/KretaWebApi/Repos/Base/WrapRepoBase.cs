using APIHelpersLibrary.Paged;
using KretaCommandLine.API;
using KretaCommandLine.DTO;
using KretaCommandLine.Model;
using KretaCommandLine.Model.Abstract;
using KretaCommandLine.QueryParameter;
using Microsoft.EntityFrameworkCore;

namespace KretaWebApi.Repos.Base
{
    public class WrapRepoBase<TDbContext> : IWrapRepoBase where TDbContext : DbContext
    {
        private IDbContextFactory<TDbContext> _dbContextFactory;

        public WrapRepoBase(IDbContextFactory<TDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public DbSet<TEntity>? DbSet<TEntity>() where TEntity : class, new()
        {
            var dbContext = _dbContextFactory.CreateDbContext();
            return dbContext.GetDbSet<TEntity>();
        }

        public List<NumberOfStudentInClass> GetNumberOfStudentPerClass()
        {
            DbSet<SchoolClass>? schoolClassesSet = DbSet<SchoolClass>();
            DbSet<Student>? studentsSet = DbSet<Student>();

            List<NumberOfStudentInClass> result = new List<NumberOfStudentInClass>();


            var result2 = from schoolClass in schoolClassesSet
                          from students in studentsSet
                          group schoolClass by new { schoolClass.SchoolYear, schoolClass.ClassType } into schoolClassGroup
                          select new NumberOfStudentInClass(schoolClassGroup.Key.SchoolYear, schoolClassGroup.Key.ClassType, schoolClassGroup.Count());
                          

            /*if (schoolClassesSet is object)
            {
                foreach (SchoolClass schoolClass in schoolClassesSet)
                {
                    if (studentsSet is object)
                    {
                        int count = studentsSet.Count(student => student.SchoolClassId == schoolClass.Id);
                        result.Add(new NumberOfStudentInClass(schoolClass.SchoolYear, schoolClass.ClassType, count));
                    }
                }
            }*/
            return new List<NumberOfStudentInClass>();
        }

    }
}
