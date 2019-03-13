using Dangl.Data.Shared.Json;
using Newtonsoft.Json;
using System;
using Xunit;

namespace Dangl.Data.Shared.Tests.Json
{
    public class DefaultValuesContractResolverTests
    {
        [Fact]
        public void SerializesDateTime()
        {
            var input = new
            {
                DateTimeProp = DateTime.UtcNow
            };
            var serialized = GetSerializedJson(input);
            Assert.Contains("DateTimeProp", serialized);
        }

        [Fact]
        public void SerializesDateTimeOffset()
        {
            var input = new
            {
                DateTimeOffsetProp = DateTimeOffset.UtcNow
            };
            var serialized = GetSerializedJson(input);
            Assert.Contains("DateTimeOffsetProp", serialized);
        }

        [Fact]
        public void SerializesGuid()
        {
            var input = new
            {
                GuidProp = Guid.NewGuid()
            };
            var serialized = GetSerializedJson(input);
            Assert.Contains("GuidProp", serialized);
        }

        [Fact]
        public void DoesNotSerializeDefaultDateTime()
        {
            var input = new
            {
                DateTimeProp = default(DateTime)
            };
            var serialized = GetSerializedJson(input);
            Assert.DoesNotContain("DateTimeProp", serialized);
        }

        [Fact]
        public void DoesNotSerializeDefaultDateTimeOffset()
        {
            var input = new
            {
                DateTimeOffsetProp = default(DateTimeOffset)
            };
            var serialized = GetSerializedJson(input);
            Assert.DoesNotContain("DateTimeOffsetProp", serialized);
        }

        [Fact]
        public void DoesNotSerializeDefaultGuid()
        {
            var input = new
            {
                GuidProp = default(Guid)
            };
            var serialized = GetSerializedJson(input);
            Assert.DoesNotContain("GuidProp", serialized);
        }

        private string GetSerializedJson(object input)
        {
            var jsonOptions = new JsonSerializerSettings();
            jsonOptions.ContractResolver = new DefaultValuesContractResolver();
            var serialized = JsonConvert.SerializeObject(input, jsonOptions);
            return serialized;
        }
    }
}
