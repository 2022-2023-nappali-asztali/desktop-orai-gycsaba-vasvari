using KretaCommandLine.Model;
using KretaCommandLine.Model.Abstract;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace KretaDesktop.Services
{
    public class StudentAPIService : APIService, IStudentAPIService
    {
         public async ValueTask<List<Student>> SelectStudentOfClass<TEntity>(long schoolClassId)  where TEntity : Student, new ()
         {
            List<Student> result = new List<Student>();
            HttpClient client = new HttpClient();
            client.BaseAddress = APIURLExtension.GetHttpClientUri();
            if (client is object)
            {
                string path = APIURLExtension.SetRelativeUrl<TEntity>();
                result = await client.GetFromJsonAsync<List<Student>>($"{path}/byclass/{schoolClassId}");
                if (result is object)
                    return result;
            }
            return result;
        }
    }
}
