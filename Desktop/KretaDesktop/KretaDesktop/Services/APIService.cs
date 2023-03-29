using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using KretaCommandLine.Model.Abstract;
using APIHelpersLibrary.Paged;
using Microsoft.AspNetCore.WebUtilities;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Linq;
using KretaCommandLine.API;

namespace KretaDesktop.Services
{
    public class APIService : IAPIService
    {
        public async ValueTask<PagingResponse<TEntity>> GetPageAsync<TEntity>(ItemParameters parameters) where TEntity : ClassWithId, new()
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = APIURLExtension.GetHttpClientUri();
            if (client is object)
            {
                Dictionary<string, string> queryParameters = new Dictionary<string, string>()
                {
                    ["pageNumber"] = parameters.PageNumber.ToString(),
                    ["pageSize"] = parameters.PageSize.ToString()
                };

                string path = APIURLExtension.SetRelativeUrl<TEntity>();
                var response = await client.GetAsync(QueryHelpers.AddQueryString($"{path}/getpaged", queryParameters));
                var content = await response.Content.ReadAsStringAsync();
                if (response is object && response.Headers is object && response.IsSuccessStatusCode)
                {
                    var headerValues = response.Headers.GetValues("X-Pagination").First<string>();
                    MetaData? medaData = JsonConvert.DeserializeObject<MetaData>(headerValues);
                    if (medaData is object)
                    {
                        PagingResponse<TEntity> pagingResponse = new PagingResponse<TEntity>
                        {
                            Items = JsonConvert.DeserializeObject<List<TEntity>>(content),
                            MetaData = medaData
                        };
                        return pagingResponse;
                    }
                }
            }
            return new PagingResponse<TEntity>();
        }

        public async ValueTask<List<TEntity>> SelectAllRecordAsync<TEntity>() where TEntity : ClassWithId, new()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = APIURLExtension.GetHttpClientUri();            
            if (client is object)
            {
                string path = APIURLExtension.SetRelativeUrl<TEntity>();
                List<TEntity>? result = await client.GetFromJsonAsync<List<TEntity>>(path);
                if (result is object)
                    return result;
                else
                    return new List<TEntity>();
            }
            else
                return new List<TEntity>();
        }

        public async ValueTask<TEntity> GetBy<TEntity>(long id) where TEntity : ClassWithId, new()
        {
            TEntity? result = new TEntity();
            HttpClient client = new HttpClient();
            client.BaseAddress = APIURLExtension.GetHttpClientUri();
            if (client is object)
            {
                string path = APIURLExtension.SetRelativeUrl<TEntity>();
                result = await client.GetFromJsonAsync<TEntity>($"{path}/{id}");
                if (result is object)
                    return result;
                else
                    result = new TEntity();
            }
            return result;
        }

        public async ValueTask<APICallState> Save<TEntity>(TEntity item) where TEntity : ClassWithId, new()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = APIURLExtension.GetHttpClientUri();             
            if (client is object)
            {
                string path = APIURLExtension.SetRelativeUrl<TEntity>();
                if (item.HasId)
                {
                    HttpResponseMessage response = await client.PutAsJsonAsync(path, item);
                    if (response is object && response.IsSuccessStatusCode)
                        return APICallState.Success;
                }
                else
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync(path, item);
                    if (response is object && response.IsSuccessStatusCode)
                        return APICallState.Success;
                }
            }
            return APICallState.SaveFaild;
        }

        public async ValueTask<APICallState> Delete<TEntity>(long id) where TEntity : ClassWithId, new()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = APIURLExtension.GetHttpClientUri();
            if (client is object)
            {
                string path = APIURLExtension.SetRelativeUrl<TEntity>();
                HttpResponseMessage response = await client.DeleteAsync($"{path}/{id}");
                if (response is object && response.IsSuccessStatusCode)
                    return APICallState.Success;
            }
            return APICallState.DeleteFail;
        }
    }
}
