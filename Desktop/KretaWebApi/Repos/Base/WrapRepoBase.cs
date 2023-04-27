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
        private DbSet<Teacher>? _teachersSet;
        private DbSet<TeachTeacherSubject>? _teachTeacherSubjectSet;
        private DbSet<Subject>? _subjectsSet;

        private IDbContextFactory<TDbContext> _dbContextFactory;

        public WrapRepoBase(IDbContextFactory<TDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
            var dbContext = _dbContextFactory.CreateDbContext();
            
            _schoolClassesSet= dbContext.GetDbSet<SchoolClass>();
            _studentsSet = dbContext.GetDbSet<Student>();
            _teachersSet = dbContext.GetDbSet<Teacher>();
            _subjectsSet = dbContext.GetDbSet<Subject>();

            _teachTeacherSubjectSet = dbContext.GetDbSet<TeachTeacherSubject>();
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
            }
            return result;
        }

        public async ValueTask<List<SchoolClass>> GetSchoolClassWithNoStudent()
        {
            List<SchoolClass> result = new List<SchoolClass>();

            if (_schoolClassesSet is object && _studentsSet is object)
            {
                 result = await
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
                              ).ToListAsync();               
            }
            return result;
        }

        public async ValueTask<List<Subject>> GetTeacherSubjects(int teacherId)
        {
            List<Subject> result = new List<Subject>();

            if (_teachersSet is object && _subjectsSet is object && _teachTeacherSubjectSet is object)
            {
                result = await
                        (from teacher in _teachersSet
                         from subject in _subjectsSet
                         from teachTeacherSubject in _teachTeacherSubjectSet
                         where teacher.Id == teachTeacherSubject.TeacherId &&
                           subject.Id == teachTeacherSubject.SubjectId &&
                           teacher.Id == teacherId
                         select subject
                         ).ToListAsync();
            }
            return result;

        }

    }
}
