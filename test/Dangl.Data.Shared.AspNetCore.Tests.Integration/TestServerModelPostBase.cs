using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Dangl.Data.Shared.AspNetCore.Tests.Integration
{
    public abstract class TestServerModelPostBase<TModel> : TestServerBase
    {
        protected async Task SendJsonRequest(TModel model)
        {
            var client = GetClient();
            var url = GetUrl();
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            var jsonModel = model == null ? string.Empty : JsonConvert.SerializeObject(model);
            request.Content = new StringContent(jsonModel, Encoding.UTF8, "application/json");
            _response = await client.SendAsync(request);
            await DeserializeApiError();
        }
    }
}
