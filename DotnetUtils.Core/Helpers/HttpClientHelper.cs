using DotnetUtils.Core.Contracts;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace DotnetUtils.Core.Helpers
{
    public class HttpClientHelper : IHttpClientHelper
    {
        private HttpClient addHeaders(HttpClient client, Dictionary<string, string> headers)
        {
            if (headers != null && headers.Count > 0)
            {
                foreach (KeyValuePair<string, string> header in headers)
                {
                    client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }

            return client;
        }

        public string AppendQueryStrings(string baseUrl, Dictionary<string, string> queryStringCollection = null)
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            if (queryStringCollection != null)
            {
                foreach (var query in queryStringCollection)
                {
                    queryString.Add(query.Key, query.Value);
                }

                return string.Format("{0}?{1}", baseUrl, queryString);
            }

            return baseUrl;
        }
        private async Task<BaseApiResponse<T>> translateToApiBase<T>(HttpResponseMessage wResponse)
        {
            BaseApiResponse<T> response = new BaseApiResponse<T>(wResponse);

            using (wResponse)
            {
                response.Result = JsonConvert.DeserializeObject<T>(await wResponse.Content.ReadAsStringAsync());
            }

            return response;
        }


        public async Task<HttpResponseMessage> GET(string url, Dictionary<string, string> queryStrings, Dictionary<string, string> headers)
        {
            using (var client = new HttpClient())
            {
                addHeaders(client, headers);

                url = AppendQueryStrings(url, queryStrings);

                return await client.GetAsync(url);
            }
        }

        public async Task<BaseApiResponse<T>> GET<T>(string url, Dictionary<string, string> queryStrings, Dictionary<string, string> headers)
        {
            return await translateToApiBase<T>(await GET(url, queryStrings, headers));
        }

        public async Task<BaseApiResponse<T>> GET<T>(string url)
        {
            return await translateToApiBase<T>(await GET(url, null, null));
        }
    }
}
