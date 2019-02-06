using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Dangl.Data.Shared.AspNetCore.Tests.Integration
{
    public class EmptyFormFileValidatorTests
    {
        public class FormFile : TestServerFormFileBase
        {
            protected override string GetUrl() => "FormFile";

            [Fact]
            public async Task NoApiErrorForCorrectModel()
            {
                await SendFormFileRequest(true, false);
                Assert.True(_response.IsSuccessStatusCode);
            }

            [Fact]
            public async Task ApiErrorForMissingModel()
            {
                await SendFormFileRequest(true, true);
                Assert.False(_response.IsSuccessStatusCode);
                Assert.NotEmpty(_responseApiError.Errors);
                Assert.Contains(_responseApiError.Errors, err => err.Value.Any(errMsg => errMsg.Contains("zero bytes")));
            }
        }
    }
}
