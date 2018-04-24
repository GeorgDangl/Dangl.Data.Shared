using Dangl.Data.Shared.Validation;

namespace Dangl.Data.Shared.AspNetCore.Tests.Integration
{
    public class ModelWithRequirement
    {
        [BiggerThanZero]
        public int Value { get; set; }
    }
}
