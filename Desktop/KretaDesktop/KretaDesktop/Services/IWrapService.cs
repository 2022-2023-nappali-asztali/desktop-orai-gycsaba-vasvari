using KretaCommandLine.DTO;
using KretaCommandLine.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KretaDesktop.Services
{
    public interface IWrapService
    {
        public ValueTask<List<NumberOfStudentInClass>> NumberOfStudentPerClass();
        public ValueTask<List<SchoolClass>> SchoolClassWithNoStudent();
    }
}
