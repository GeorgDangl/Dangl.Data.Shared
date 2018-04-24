using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Dangl.Data.Shared.AspNetCore.Tests.Integration
{
    public class RequiredFormFileValidationFilterTests
    {
        public class RequiredFormFile : TestServerFormFileBase
        {
            protected override string GetUrl() => "RequiredFormFile";

            [Fact]
            public async Task NoApiErrorForCorrectModel()
            {
                await SendFormFileRequest(true);
                Assert.True(_response.IsSuccessStatusCode);
            }

            [Fact]
            public async Task ApiErrorForMissingModel()
            {
                await SendFormFileRequest(false);
                Assert.False(_response.IsSuccessStatusCode);
                Assert.NotEmpty(_responseApiError.Errors);
            }
        }

        public class MultipleRequiredFormFile : TestServerFormFileBase
        {
            protected override string GetUrl() => "MultipleRequiredFormFile";

            [Fact]
            public async Task ApiErrorForMissingModel()
            {
                await SendFormFileRequest(false);
                Assert.False(_response.IsSuccessStatusCode);
                Assert.Single(_responseApiError.Errors);
                Assert.Equal(2, _responseApiError.Errors.First().Value.Count());
            }
        }

        public class FormFile : TestServerFormFileBase
        {
            protected override string GetUrl() => "FormFile";

            [Fact]
            public async Task NoApiErrorForCorrectModel()
            {
                await SendFormFileRequest(true);
                Assert.True(_response.IsSuccessStatusCode);
            }

            [Fact]
            public async Task NoApiErrorForMissingModel()
            {
                await SendFormFileRequest(false);
                Assert.True(_response.IsSuccessStatusCode);
            }
        }
    }
}
