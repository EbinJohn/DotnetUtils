using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace DotnetUtils.Core.Helpers.Tests
{
    [TestClass()]
    public class HttpClientHelperTests
    {
        private IHttpClientHelper _http;
        [TestInitialize]
        public void Setup()
        {
            _http = new HttpClientHelper();
        }

        [TestCleanup]
        public void TearDown()
        {
            _http = null;
        }

        [TestMethod()]
        public void AppendQueryStrings_Test()
        {
            Dictionary<string, string> queryString = new Dictionary<string, string>();
            queryString.Add("search", "testThis");

            string url = "www.google.com";

            var actual = _http.AppendQueryStrings(url, queryString);

            Assert.AreEqual(actual, "www.google.com?search=testThis");
        }

        [TestMethod()]
        public async Task Invoke_GET_and_Return_Results()
        {
            var url = "https://reqres.in/api/users";

            var response = await _http.GET<dynamic>(url);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }
    }
}