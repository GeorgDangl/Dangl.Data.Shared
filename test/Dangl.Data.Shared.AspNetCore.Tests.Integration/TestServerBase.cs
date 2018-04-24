using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;

namespace Dangl.Data.Shared.AspNetCore.Tests.Integration
{
    public abstract class TestServerBase
    {
        private readonly TestServer _testServer;
        protected abstract string GetUrl();

        protected HttpResponseMessage _response;
        protected ApiError _responseApiError;

        protected TestServerBase()
        {
            _testServer = GetTestServer();
        }

        private TestServer GetTestServer()
        {
            var webHostBuilder = new WebHostBuilder()
                .UseStartup<IntegrationTestsStartup>();
            return new TestServer(webHostBuilder);
        }

        protected HttpClient GetClient()
        {
            return _testServer.CreateClient();
        }

        protected async Task DeserializeApiError()
        {
            if (!_response.IsSuccessStatusCode)
            {
                var responseContent = await _response.Content.ReadAsStringAsync();
                _responseApiError = JsonConvert.DeserializeObject<ApiError>(responseContent);
            }
        }
    }
}
