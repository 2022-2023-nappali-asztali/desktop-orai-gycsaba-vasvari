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
        private DbSet<SchoolClass>? _schoolClassesSet;
        private DbSet<Student>? _studentsSet;
        private IDbContextFactory<TDbContext> _dbContextFactory;

        public WrapRepoBase(IDbContextFactory<TDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
            var dbContext = _dbContextFactory.CreateDbContext();
            
            _schoolClassesSet= dbContext.GetDbSet<SchoolClass>();
            _studentsSet = dbContext.GetDbSet<Student>();
        }

        public async ValueTask<List<NumberOfStudentInClass>> GetNumberOfStudentPerClass()
        {
            List<NumberOfStudentInClass> result = new List<NumberOfStudentInClass>();

            if (_schoolClassesSet is object && _studentsSet is object)
            {
                result = (    from schoolClass in _schoolClassesSet
                              from students in _studentsSet
                              where students.SchoolClassId==schoolClass.Id
                              group schoolClass by new { schoolClass.SchoolYear, schoolClass.ClassType } into schoolClassGroup
                              select new NumberOfStudentInClass(schoolClassGroup.Key.SchoolYear, schoolClassGroup.Key.ClassType, schoolClassGroup.Count())
                          ).ToList();

              /*  if (_schoolClassesSet is object)
                {
                    foreach (SchoolClass schoolClass in _schoolClassesSet)
                    {
                        if (_studentsSet is object)
                        {
                            int count = _studentsSet.Count(student => student.SchoolClassId == schoolClass.Id);
                            result.Add(new NumberOfStudentInClass(schoolClass.SchoolYear, schoolClass.ClassType, count));
                        }
                    }
                }*/
            }
            return result;
        }

        public async ValueTask<List<SchoolClass>> GetSchoolClassWithNoStudents()
        {
            List<SchoolClass> result = new List<SchoolClass>();

            if (_schoolClassesSet is object && _studentsSet is object)
            {
                 result =
                              (
                                      from SchoolClass in _schoolClassesSet
                                      select SchoolClass
                              )
                              .Except
                              (                              
                                  (
                                      from schoolClass in _schoolClassesSet
                                      from students in _studentsSet
                                      where students.SchoolClassId == schoolClass.Id
                                      select schoolClass
                                  ).Distinct()
                              ).ToList();               
            }
            return result;
        }
    }
}
