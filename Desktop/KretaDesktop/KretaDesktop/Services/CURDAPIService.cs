using KretaCommandLine.APIModel;
using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KretaDesktop.Services
{
    public class CURDAPIService
    {
        public async Task<PagedList<TEntity>> GetPageAsync<TEntity>(QueryStringParameters queryString)
        {
            if (queryString == null)
                return new PagedList<TEntity>();
            HttpClient client = new HttpClient();
            client.BaseAddress=GetHttpClientUri();
            if (client!=null)
            {

                string jsonParamter=JsonConvert.SerializeObject(queryString);
                HttpContent content = new StringContent(jsonParamter,Encoding.UTF8, "application/json");

                HttpResponseMessage response=await client.PostAsync($"/api/{nameof(TEntity)}/getpaged",content);
                if (response!=null && response.IsSuccessStatusCode)
                {
                    var resultContent = await response.Content.ReadAsStringAsync();
                    PagedList<TEntity>  result= JsonConvert.DeserializeObject<PagedList<TEntity>>(resultContent);
                    return result;
                }
            }
            return new PagedList<TEntity>();
        }

        private Uri GetHttpClientUri()
        {
            UriBuilder uri = new UriBuilder();
            uri = GetAPIUri(uri);
            return uri.Uri;
        }

        private UriBuilder GetAPIUri(UriBuilder uri)
        {
            uri.Scheme = "http";
            uri.Host = "localhost";
            uri.Port = 8888;
            return uri;
        }
    }
}
