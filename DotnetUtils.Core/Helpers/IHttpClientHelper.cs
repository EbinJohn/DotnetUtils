using DotnetUtils.Core.Contracts;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace DotnetUtils.Core.Helpers
{
    public interface IHttpClientHelper
    {

        string AppendQueryStrings(string baseUrl, Dictionary<string, string> queryStringCollection = null);

        Task<BaseApiResponse<T>> GET<T>(string url);

        Task<HttpResponseMessage> GET(string url, Dictionary<string, string> queryStrings, Dictionary<string, string> headers);

        Task<BaseApiResponse<T>> GET<T>(string url, Dictionary<string, string> queryStrings, Dictionary<string, string> headers);

    }
}
