using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Dangl.Data.Shared.AspNetCore.Tests.Integration
{
    public class CdnNoCacheAttributeTests : TestServerBase
    {
        [Fact]
        public async Task ReturnsCorrectCacheControlHeaders()
        {
            await MakeRequest();
            Assert.True(_response.IsSuccessStatusCode);

            var cacheControlResponseHeader = _response.Headers.CacheControl;
            Assert.True(cacheControlResponseHeader.NoTransform);
            Assert.True(cacheControlResponseHeader.NoStore);
            Assert.True(cacheControlResponseHeader.NoCache);

        }

        private async Task MakeRequest()
        {
            var url = GetUrl();
            var client = GetClient();
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            _response = await client.SendAsync(request);
        }

        protected override string GetUrl()
        {
            return "/NoCacheNoTransform";
        }
    }
}
