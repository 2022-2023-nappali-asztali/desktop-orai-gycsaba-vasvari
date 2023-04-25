using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using APIHelpersLibrary.Paged;
using KretaCommandLine.Model.Abstract;
using Microsoft.AspNetCore.WebUtilities;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Linq;
using KretaCommandLine.API;
using KretaCommandLine.QueryParameter;
using KretaCommandLine.DTO;

namespace KretaDesktop.Services
{
    public class APIService : IAPIService
    {
        public async ValueTask<PagingResponse<TEntity>> GetPageAsync<TEntity>(PagingParameters pagingParameters, QueryParameters queryParameters=null) where TEntity : ClassWithId, new()
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = APIURLExtension.GetHttpClientUri();
            if (client is object)
            {
                Dictionary<string, string> dictionary= new Dictionary<string, string>();
                HttpResponseMessage? response = null;
                if (queryParameters is object)
                {
                    Dictionary<string, string> pagingDictionary = GetParameterDictionary(pagingParameters,queryParameters);
                    string path = APIURLExtension.SetRelativUrl<TEntity>();
                    response = await client.GetAsync(QueryHelpers.AddQueryString($"{path}/getpagedwithqueryparameters", pagingDictionary));

                }
                else
                {
                    Dictionary<string, string> pagingDictionary = GetParameterDictionary(pagingParameters);
                    string path = APIURLExtension.SetRelativUrl<TEntity>();
                    response = await client.GetAsync(QueryHelpers.AddQueryString($"{path}/getpaged", pagingDictionary));
                    
                }

                if (response is object)
                {
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
            }
            return new PagingResponse<TEntity>();
        }

        public async ValueTask<PagingResponse<TEntity>> SelectAllIncludedRecordPagedAsync<TEntity>(PagingParameters pagingParameters, QueryParameters queryParameters) where TEntity : ClassWithId, new()
        {
            HttpClient client = new HttpClient();
            string path = APIURLExtension.SetRelativUrl<TEntity>();
            client.BaseAddress = APIURLExtension.GetHttpClientUri();
            if (client is object)
            {
                Dictionary<string, string> parameter=new Dictionary<string, string>();
                HttpResponseMessage response = null;
                if (pagingParameters is object)
                {
                    parameter=GetParameterDictionary(pagingParameters,queryParameters);
                    response = await client.GetAsync(QueryHelpers.AddQueryString($"{path}/includedandpagedwithparameters", parameter));
                }
                else
                {
                    parameter = GetParameterDictionary(pagingParameters);
                    response = await client.GetAsync(QueryHelpers.AddQueryString($"{path}/includedandpaged", parameter));
                }
                if (response is object)
                {
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
            }
            return new PagingResponse<TEntity>();
        }

        public async ValueTask<List<TEntity>> SelectAllRecordAsync<TEntity>(QueryParameters? queryParameters=null) where TEntity : ClassWithId, new()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = APIURLExtension.GetHttpClientUri();            
            if (client is object)
            {
                string path = APIURLExtension.SetRelativUrl<TEntity>();
                if (queryParameters is object)
                {
                    Dictionary<string,string> parameter=GetParameterDictionary(queryParameters);
                    HttpResponseMessage response = await client.GetAsync(QueryHelpers.AddQueryString($"{path}/withqueryparameters",parameter));
                    if (response is object && response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<List<TEntity>>(content);
                    }
                }
                else
                {
                    List<TEntity>? result = await client.GetFromJsonAsync<List<TEntity>>(path);
                    if (result is object)
                        return result;
                }
            }
            return new List<TEntity>();
        }

        public async ValueTask<List<TEntity>> SelectAllIncludedRecordAsync<TEntity>(QueryParameters? queryParameters=null) where TEntity : ClassWithId, new()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = APIURLExtension.GetHttpClientUri();
            if (client is object)
            {
                string path = APIURLExtension.SetRelativUrl<TEntity>();
                if (queryParameters is object && (queryParameters.SearchTerm != null || queryParameters.OrderBy != null))
                {
                    Dictionary<string, string> parameter = GetParameterDictionary(queryParameters);
                    HttpResponseMessage response = await client.GetAsync(QueryHelpers.AddQueryString($"{path}/includedwithparameters", parameter));                   
                    if (response is object && response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<List<TEntity>>(content);
                    }
                }
                else
                {
                    HttpResponseMessage response = await client.GetAsync($"{path}/included");
                    if (response is object && response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<List<TEntity>>(content);
                    }
                }
            }
            return new List<TEntity>();
        }

        public async ValueTask<TEntity> GetBy<TEntity>(long id) where TEntity : ClassWithId, new()
        {
            TEntity? result = new TEntity();
            HttpClient client = new HttpClient();
            client.BaseAddress = APIURLExtension.GetHttpClientUri();
            if (client is object)
            {
                string path = APIURLExtension.SetRelativUrl<TEntity>();
                result = await client.GetFromJsonAsync<TEntity>($"{path}/{id}");
                if (result is object)
                    return result;
            }
            return result;
        }

        public async ValueTask<int> GetCountOf<TEntity>() where TEntity : ClassWithId, new()
        {
            HttpClient client = new HttpClient();
            int result = 0;
            client.BaseAddress = APIURLExtension.GetHttpClientUri();
            if (client is object)
            {
                string path = APIURLExtension.SetRelativUrl<TEntity>();
                result = await client.GetFromJsonAsync<int>($"{path}/count");
            }
            return result;
        }


        public async ValueTask<APICallState> Save<TEntity>(TEntity item) where TEntity : ClassWithId, new()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = APIURLExtension.GetHttpClientUri();             
            if (client is object)
            {
                string path = APIURLExtension.SetRelativUrl<TEntity>();
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
                string path = APIURLExtension.SetRelativUrl<TEntity>();
                HttpResponseMessage response = await client.DeleteAsync($"{path}/{id}");
                if (response is object && response.IsSuccessStatusCode)
                    return APICallState.Success;
            }
            return APICallState.DeleteFail;
        }

        private Dictionary<string, string> GetParameterDictionary(PagingParameters pagingParameters)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>()
            {
                [nameof(pagingParameters.PageNumber)] = pagingParameters.PageNumber.ToString(),
                [nameof(pagingParameters.PageSize)] = pagingParameters.PageSize.ToString()
            };
            return dictionary;
        }

        private Dictionary<string,string> GetParameterDictionary(QueryParameters queryParameters)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>()
            {
                [nameof(queryParameters.SearchPropertyName)]=queryParameters.SearchPropertyName,
                [nameof(queryParameters.SearchTerm)] = queryParameters.SearchTerm,
                [nameof(queryParameters.OrderBy)] = queryParameters.OrderBy
            };
            return dictionary;
        }

        private Dictionary<string, string> GetParameterDictionary(PagingParameters pagingParameters, QueryParameters queryParameters)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>()
            {
                [nameof(pagingParameters.PageNumber)] = pagingParameters.PageNumber.ToString(),
                [nameof(pagingParameters.PageSize)] = pagingParameters.PageSize.ToString(),
                [nameof(queryParameters.SearchPropertyName)] = queryParameters.SearchPropertyName,
                [nameof(queryParameters.SearchTerm)] = queryParameters.SearchTerm,
                [nameof(queryParameters.OrderBy)] = queryParameters.OrderBy
            };
            return dictionary;
        }

    }
}
