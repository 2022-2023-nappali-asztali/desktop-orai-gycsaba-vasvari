using APIHelpersLibrary.Paged;
using KretaCommandLine.API;
using KretaCommandLine.DTO;
using KretaCommandLine.Model;
using KretaCommandLine.Model.Abstract;
using KretaCommandLine.QueryParameter;

namespace KretaWebApi.Repos.Base
{
    public interface IWrapRepoBase
    {
        public ValueTask<List<NumberOfStudentInClass>> GetNumberOfStudentPerClass();
    }
}
