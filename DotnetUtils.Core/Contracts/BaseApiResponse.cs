using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace DotnetUtils.Core.Contracts
{
    public class BaseApiResponse<T>
    {
        public WebHeaderCollection Headers { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public T Result { get; set; }

        public BaseApiResponse(HttpResponseMessage response)
        {
            Headers = getResponseHeaders(response.Headers);
            StatusCode = response.StatusCode;


        }

        private WebHeaderCollection getResponseHeaders(HttpResponseHeaders headers)
        {
            WebHeaderCollection newHeaders = new WebHeaderCollection();
            foreach (KeyValuePair<string, IEnumerable<string>> headerPair in headers)
            {
                foreach (string headerValue in headerPair.Value)
                {
                    newHeaders.Add(headerPair.Key, headerValue);
                }
            }
            return newHeaders;
        }
    }
}
