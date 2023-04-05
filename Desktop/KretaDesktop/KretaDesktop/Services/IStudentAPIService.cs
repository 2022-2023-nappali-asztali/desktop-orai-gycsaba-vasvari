using KretaCommandLine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KretaDesktop.Services
{
    public interface IStudentAPIService
    {
        public ValueTask<List<Student>> SelectStudentOfClass<TEntity>(long schoolClassId) where TEntity : Student, new();
    }
}
