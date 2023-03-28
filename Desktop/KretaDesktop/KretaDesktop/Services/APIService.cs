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
        public async Task<PagingResponse<TEntity>> GetPageAsync<TEntity>(ItemParameters parameters) where TEntity : ClassWithId, new()
        {
           
            HttpClient client = new HttpClient();
            string relativUrl = RelativeUrlExtension.SetRelativeUrl<TEntity>();
            if (client is object)
            {
                Dictionary<string, string> queryParameters = new Dictionary<string, string>()
                {
                    ["pageNumber"] = parameters.PageNumber.ToString(),
                    ["pageSize"] = parameters.PageSize.ToString()
                };

                var response = await client.GetAsync(QueryHelpers.AddQueryString($"{relativUrl}/getpaged", queryParameters));
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
            string relativUrl = RelativeUrlExtension.SetRelativeUrl<TEntity>();
            if (client is object)
            { 
                List<TEntity>? result = await client.GetFromJsonAsync<List<TEntity>>(relativUrl);
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
            string relativUrl = RelativeUrlExtension.SetRelativeUrl<TEntity>();
            if (client is object)
            {
                result = await client.GetFromJsonAsync<TEntity>($"{relativUrl}/{id}");
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
            string relativUrl = RelativeUrlExtension.SetRelativeUrl<TEntity>();
            if (client is object)
            { 
                if (item.HasId)
                {
                    HttpResponseMessage response = await client.PutAsJsonAsync(relativUrl, item);
                    if (response is object && response.IsSuccessStatusCode)
                        return APICallState.Success;
                }
                else
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync(relativUrl, item);
                    if (response is object && response.IsSuccessStatusCode)
                        return APICallState.Success;
                }
            }
            return APICallState.SaveFaild;
        }

        public async ValueTask<APICallState> Delete<TEntity>(long id) where TEntity : ClassWithId, new()
        {
            HttpClient client = new HttpClient();
            string relativUrl = RelativeUrlExtension.SetRelativeUrl<TEntity>();
            if (client is object)
            { 
                HttpResponseMessage response = await client.DeleteAsync($"{relativUrl}/{id}");
                if (response is object && response.IsSuccessStatusCode)
                    return APICallState.Success;
            }
            return APICallState.DeleteFail;
        }

        public static class RelativeUrlExtension
        {
            public static string SetRelativeUrl<TEntity>() where TEntity : ClassWithId, new()
            {
                return $"/{typeof(TEntity).Name}/api";
            }
        }
    }
}
