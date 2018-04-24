using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dangl.Data.Shared.AspNetCore.Tests.Integration
{
    public abstract class TestServerFormFileBase : TestServerBase
    {
        protected async Task SendFormFileRequest(bool sendFormFile)
        {
            var client = GetClient();
            var url = GetUrl();
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            var multiPartFormContent = new MultipartFormDataContent();
            var testFileStream = new MemoryStream(new byte[24]);
            var streamContent = new StreamContent(testFileStream);
            streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
            var fileName = sendFormFile ? "formFile" : "unexpectedFile";
            multiPartFormContent.Add(streamContent, fileName, "uploaded-file.bin");
            request.Content = multiPartFormContent;

            _response = await client.SendAsync(request);
            await DeserializeApiError();
        }
    }
}
