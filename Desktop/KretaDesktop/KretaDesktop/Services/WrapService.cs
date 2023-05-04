using KretaCommandLine.DTO;
using KretaCommandLine.Model;
using KretaCommandLine.QueryParameter;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection.Metadata;
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
                HttpResponseMessage response = await client.GetAsync($"{path}/schoolclasswithnostudent");
                if (response is object && response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<SchoolClass>>(content);
                };
            }
            return new List<SchoolClass>();
        }

        public async ValueTask<List<Subject>> GetTeacherSubjects(long teacherId)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = APIURLExtension.GetHttpClientUri();
            if (client is object)
            {
                string path = APIURLExtension.SetRelativUrl();
                HttpResponseMessage response = await client.GetAsync($"{path}/teachersubjects/{teacherId}");
                if (response is object && response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Subject>>(content);
                }
            }
            return new List<Subject>();
        }
    }
}
