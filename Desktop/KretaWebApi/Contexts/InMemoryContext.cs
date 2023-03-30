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
            if (Settings.Count() > 0)
                return;

            try
            {
                if (Settings is object)
                {
                    Settings.Add(new Settings(1, 10));
                }                
                if (Address is object)
                {
                    Address.Add(new Address(1, "Valami utca 1.", "Varos", 1000));
                    Address.Add(new Address(2, "Valami utca 2.", "Varos", 1000));
                    Address.Add(new Address(3, "Valami utca 3.", "Varos", 1000));
                    Address.Add(new Address(4, "Valami utca 4.", "Varos", 1000));
                    Address.Add(new Address(5, "Valami utca 5.", "Varos", 1000));
                    Address.Add(new Address(6, "Valami utca 6.", "Varos", 1000));
                    Address.Add(new Address(7, "Valami utca 7.", "Varos", 1000));
                    Address.Add(new Address(8, "Valami utca 8.", "Varos", 1000));
                    Address.Add(new Address(9, "Valami utca 9.", "Varos", 1000));
                    Address.Add(new Address(10, "Valami utca 10.", "Varos", 1000));
                    Address.Add(new Address(11, "Valami utca 11.", "Varos", 1000));
                    Address.Add(new Address(12, "Valami utca 12.", "Varos", 1000));
                    Address.Add(new Address(13, "Valami utca 13.", "Varos", 1000));
                    Address.Add(new Address(14, "Valami utca 14.", "Varos", 1000));
                    Address.Add(new Address(15, "Valami utca 15.", "Varos", 1000));
                    Address.Add(new Address(16, "Valami utca 16.", "Varos", 1000));
                    Address.Add(new Address(17, "Valami utca 17.", "Varos", 1000));
                    Address.Add(new Address(18, "Valami utca 10.", "Varos", 1000));
                    Address.Add(new Address(19, "Valami utca 19.", "Varos", 1000));
                    Address.Add(new Address(20, "Valami utca 20.", "Varos", 1000));
                }
                if (Parent is object)
                {
                    Parent.Add(new Parent(1, "Kis Feri", true));
                    Parent.Add(new Parent(2, "Nagy János", false));
                    Parent.Add(new Parent(3, "Nagy Olga", true));
                    Parent.Add(new Parent(4, "Kis Szonja", true));
                    Parent.Add(new Parent(5, "Péter Ulrich", false));
                    Parent.Add(new Parent(6, "Kovács Richárd", true));
                    Parent.Add(new Parent(7, "Szántó Aliz", true));
                    Parent.Add(new Parent(8, "Kovács Nelli", false));
                    Parent.Add(new Parent(9, "Szántó Margit", true));
                    Parent.Add(new Parent(10, "Pék Fanni", true));
                }
                if (SchoolClass is object)
                {
                    SchoolClass.Add(new SchoolClass(1, 9, 'a', 1));
                    SchoolClass.Add(new SchoolClass(2, 9, 'b', 3));
                    SchoolClass.Add(new SchoolClass(3, 9, 'c', 5));
                    SchoolClass.Add(new SchoolClass(4, 10, 'a', 6));
                    SchoolClass.Add(new SchoolClass(5, 10, 'b', 7));
                    SchoolClass.Add(new SchoolClass(6, 10, 'c', 8));
                    SchoolClass.Add(new SchoolClass(7, 11, 'a', 9));
                }
                if (Student is object)
                {
                    Student.Add(new Student(1, "Felelő Feri", 1, 12));
                    Student.Add(new Student(2, "Jegyíró János", 1, 11));
                    Student.Add(new Student(3, "Író Ilon", 1, 13));
                    Student.Add(new Student(4, "Számoló Szinti", 2, 14));
                    Student.Add(new Student(5, "Ugrató Ulrich", 2, 15));
                    Student.Add(new Student(6, "Reszkess Richárd", 2, 16));
                    Student.Add(new Student(7, "Alkotó Aliz", 2, 17));
                    Student.Add(new Student(8, "Német Nelli", 3, 18));
                    Student.Add(new Student(9, "Megértő Mari", 3, 19));
                    Student.Add(new Student(10, "Felelő Fanni", 3, 20));
                }
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
                if (Teacher is object)
                {
                    Teacher.Add(new Teacher(1, "Feldolgozó Feri", true, false, 2));
                    Teacher.Add(new Teacher(2, "Jegyölő János", false, false, 1));
                    Teacher.Add(new Teacher(3, "Olvasó Olga", true, true, 3));
                    Teacher.Add(new Teacher(4, "Számoló Szonja", true, true, 4));
                    Teacher.Add(new Teacher(5, "Utazó Ulrich", false, false, 5));
                    Teacher.Add(new Teacher(6, "Részeg Richárd", true, false, 6));
                    Teacher.Add(new Teacher(7, "Álmodozó Aliz", true, true, 7));
                    Teacher.Add(new Teacher(8, "Nagy Nelli", false, true, 8));
                    Teacher.Add(new Teacher(9, "Megértő Margit", true, true, 9));
                    Teacher.Add(new Teacher(10, "Fuldokló Fanni", true, true, 10));
                }
                if (TeachTeacherSchoolClass is object)
                {
                    TeachTeacherSchoolClass.Add(new TeachTeacherSchoolClass(1, 1));
                    TeachTeacherSchoolClass.Add(new TeachTeacherSchoolClass(2, 2));
                    TeachTeacherSchoolClass.Add(new TeachTeacherSchoolClass(3, 7));
                    TeachTeacherSchoolClass.Add(new TeachTeacherSchoolClass(4, 3));
                    TeachTeacherSchoolClass.Add(new TeachTeacherSchoolClass(5, 4));
                    TeachTeacherSchoolClass.Add(new TeachTeacherSchoolClass(3, 6));
                    TeachTeacherSchoolClass.Add(new TeachTeacherSchoolClass(2, 4));
                    TeachTeacherSchoolClass.Add(new TeachTeacherSchoolClass(1, 2));
                    TeachTeacherSchoolClass.Add(new TeachTeacherSchoolClass(5, 1));
                    TeachTeacherSchoolClass.Add(new TeachTeacherSchoolClass(7, 2));
                    TeachTeacherSchoolClass.Add(new TeachTeacherSchoolClass(8, 2));
                    TeachTeacherSchoolClass.Add(new TeachTeacherSchoolClass(9, 1));
                    TeachTeacherSchoolClass.Add(new TeachTeacherSchoolClass(10, 2));
                    TeachTeacherSchoolClass.Add(new TeachTeacherSchoolClass(10, 3));
                    TeachTeacherSchoolClass.Add(new TeachTeacherSchoolClass(2, 5));
                    TeachTeacherSchoolClass.Add(new TeachTeacherSchoolClass(8, 6));
                    TeachTeacherSchoolClass.Add(new TeachTeacherSchoolClass(9, 7));
                    TeachTeacherSchoolClass.Add(new TeachTeacherSchoolClass(4, 5));
                    TeachTeacherSchoolClass.Add(new TeachTeacherSchoolClass(7, 1));
                    TeachTeacherSchoolClass.Add(new TeachTeacherSchoolClass(4, 7));
                    TeachTeacherSchoolClass.Add(new TeachTeacherSchoolClass(8, 4));
                    TeachTeacherSchoolClass.Add(new TeachTeacherSchoolClass(6, 3));
                    TeachTeacherSchoolClass.Add(new TeachTeacherSchoolClass(9, 4));
                    TeachTeacherSchoolClass.Add(new TeachTeacherSchoolClass(3, 1));
                    TeachTeacherSchoolClass.Add(new TeachTeacherSchoolClass(2, 9));
                }
                if (TeachTeaherSubject is object)
                {

                    TeachTeaherSubject.Add(new TeachTeacherSubject(1, 1));
                    TeachTeaherSubject.Add(new TeachTeacherSubject(1, 2));
                    TeachTeaherSubject.Add(new TeachTeacherSubject(1, 7));
                    TeachTeaherSubject.Add(new TeachTeacherSubject(2, 3));
                    TeachTeaherSubject.Add(new TeachTeacherSubject(2, 4));
                    TeachTeaherSubject.Add(new TeachTeacherSubject(2, 10));
                    TeachTeaherSubject.Add(new TeachTeacherSubject(3, 8));
                    TeachTeaherSubject.Add(new TeachTeacherSubject(4, 2));
                    TeachTeaherSubject.Add(new TeachTeacherSubject(5, 1));
                    TeachTeaherSubject.Add(new TeachTeacherSubject(5, 8));
                    TeachTeaherSubject.Add(new TeachTeacherSubject(5, 2));
                    TeachTeaherSubject.Add(new TeachTeacherSubject(6, 1));
                    TeachTeaherSubject.Add(new TeachTeacherSubject(6, 2));
                    TeachTeaherSubject.Add(new TeachTeacherSubject(6, 7));
                    TeachTeaherSubject.Add(new TeachTeacherSubject(7, 8));
                    TeachTeaherSubject.Add(new TeachTeacherSubject(8, 10));
                    TeachTeaherSubject.Add(new TeachTeacherSubject(8, 9));
                    TeachTeaherSubject.Add(new TeachTeacherSubject(8, 3));
                    TeachTeaherSubject.Add(new TeachTeacherSubject(9, 1));
                    TeachTeaherSubject.Add(new TeachTeacherSubject(9, 7));
                    TeachTeaherSubject.Add(new TeachTeacherSubject(9, 4));
                    TeachTeaherSubject.Add(new TeachTeacherSubject(10, 3));
                    TeachTeaherSubject.Add(new TeachTeacherSubject(10, 4));
                    TeachTeaherSubject.Add(new TeachTeacherSubject(10, 1));
                    TeachTeaherSubject.Add(new TeachTeacherSubject(10, 8));
                }
                if (TypeOfSubject is object)
                {
                    TypeOfSubject.Add(new TypeOfSubject(1, "Természettudomány"));
                    TypeOfSubject.Add(new TypeOfSubject(2, "Nyelv és idegen nyelv"));
                    TypeOfSubject.Add(new TypeOfSubject(3, "Társadalomtuduomány"));
                    TypeOfSubject.Add(new TypeOfSubject(4, "Művészet"));
                }
                SaveChanges();
            }
            catch
            { }
        }
    }
}
