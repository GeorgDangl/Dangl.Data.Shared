using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Dangl.Data.Shared.AspNetCore.Tests.Integration
{
    public class ModelStateValidationFilterTests
    {
        public class Model : TestServerModelPostBase<ModelWithoutRequirement>
        {
            protected override string GetUrl() => "Model";

            [Fact]
            public async Task NoApiErrorForCorrectModel()
            {
                var model = new ModelWithoutRequirement();
                await SendJsonRequest(model);
                Assert.True(_response.IsSuccessStatusCode);
            }

            [Fact]
            public async Task ApiErrorForMissingModel()
            {
                await SendJsonRequest(null);
                Assert.False(_response.IsSuccessStatusCode);
                Assert.NotEmpty(_responseApiError.Errors);
            }
        }

        public class ModelWithBiggerThanZeroAttribute : TestServerModelPostBase<ModelWithRequirement>
        {
            protected override string GetUrl() => "ModelWithBiggerThanZeroAttribute";

            [Fact]
            public async Task NoApiErrorForCorrectModel()
            {
                var model = new ModelWithRequirement
                {
                    Value = 5
                };
                await SendJsonRequest(model);
                Assert.True(_response.IsSuccessStatusCode);
            }

            [Fact]
            public async Task CorrectApiErrorForInvalidModel()
            {
                var model = new ModelWithRequirement
                {
                    Value = -5
                };
                await SendJsonRequest(model);
                Assert.False(_response.IsSuccessStatusCode);

                Assert.Single(_responseApiError.Errors.Where(e => e.Key == nameof(ModelWithRequirement.Value)));
            }
        }
    }
}
