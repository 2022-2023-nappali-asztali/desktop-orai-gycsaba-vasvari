using KretaCommandLine.DTO;
using KretaCommandLine.Model;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace KretaDesktop.Services
{
    public class WrapService : IWrapService
    {        
        public async ValueTask<List<NumberOfStudentInClass>> NumberOfStudentPerClass()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = APIURLExtension.GetHttpClientUri();
            if (client is object)
            {
                string path = APIURLExtension.SetRelativUrl<Student>();
                List<NumberOfStudentInClass>? result = await client.GetFromJsonAsync<List<NumberOfStudentInClass>>($"{path}/numberofstudentperclass");
                if (result is object)
                    return result;            
            }
            return new List<NumberOfStudentInClass>();
        }

        public async ValueTask<List<SchoolClass>> SchoolClassWithNoStudent()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = APIURLExtension.GetHttpClientUri();
            if (client is object)
            {
                string path = APIURLExtension.SetRelativUrl<Student>();
                List<SchoolClass>? result = await client.GetFromJsonAsync<List<SchoolClass>>($"{path}/schoolclasswithnostudent");
                if (result is object)
                    return result;
            }
            return new List<SchoolClass>();
        }
    }
}
