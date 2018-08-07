using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Dangl.Data.Shared.AspNetCore.Tests.Integration
{
    public class CompressedRequestMiddlewareTests : TestServerBase
    {
        private readonly ModelWithoutRequirement _payload = new ModelWithoutRequirement
        {
            Value = Guid.NewGuid().ToString()
        };
        private ModelWithoutRequirement _deserializedResponse;

        [Theory]
        [InlineData("gzip")]
        [InlineData("GZIP")]
        [InlineData("Gzip")]
        [InlineData("deflate")]
        [InlineData("Deflate")]
        public async Task CanPostGzippedRequest(string compressionMethod)
        {
            await MakeRequest(compressionMethod);
            Assert.True(_response.IsSuccessStatusCode);
            Assert.Equal(_payload.Value, _deserializedResponse.Value);
        }

        private async Task MakeRequest(string compressionMethod)
        {
            var jsonPayload = JsonConvert.SerializeObject(_payload);

            using (var httpContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json"))
            using (var compressedContent = new CompressedContent(httpContent, compressionMethod))
            {
                var client = GetClient();
                var requestMessage = new HttpRequestMessage(HttpMethod.Post, GetUrl())
                {
                    Content = compressedContent
                };
                _response = await client.SendAsync(requestMessage);

                if (_response.IsSuccessStatusCode)
                {
                    var responseString = await _response.Content.ReadAsStringAsync();
                    _deserializedResponse = JsonConvert.DeserializeObject<ModelWithoutRequirement>(responseString);
                }
            }
        }

        protected override string GetUrl()
        {
            return "/Model";
        }
    }
}
