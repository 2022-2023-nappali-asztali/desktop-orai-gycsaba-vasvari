using KretaCommandLine.DTO;
using KretaCommandLine.Model;
using Newtonsoft.Json;
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
                string path = APIURLExtension.SetRelativUrl();
                HttpResponseMessage response = await client.GetAsync($"{path}/numberofstudentperclass");
                if (response is object && response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<NumberOfStudentInClass>>(content);
                }                  
            }
            return new List<NumberOfStudentInClass>();
        }

        public async ValueTask<List<SchoolClass>> SchoolClassWithNoStudent()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = APIURLExtension.GetHttpClientUri();
            if (client is object)
            {
                string path = APIURLExtension.SetRelativUrl();
                List<SchoolClass>? result = await client.GetFromJsonAsync<List<SchoolClass>>($"{path}/schoolclasswithnostudent");
                if (result is object)
                    return result;
            }
            return new List<SchoolClass>();
        }
    }
}
