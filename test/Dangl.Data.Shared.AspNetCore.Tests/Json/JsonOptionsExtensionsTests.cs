using Dangl.Data.Shared.AspNetCore.Json;
using Newtonsoft.Json;
using System;
using Xunit;

namespace Dangl.Data.Shared.AspNetCore.Tests.Json
{
    public class JsonOptionsExtensionsTests
    {
        [Fact]
        public void SerializesEnumAsString()
        {
            var jsonOptions = new JsonSerializerSettings();
            jsonOptions.ConfigureDefaultJsonSerializerSettings();
            var input = new DtoTestClass { EnumProp = TestEnum.Two };
            var serialized = JsonConvert.SerializeObject(input, jsonOptions);
            Assert.Contains("\"Two\"", serialized);
        }

        [Fact]
        public void DeserializesEnumAsString()
        {
            var deserialized = GetDeserialized("{\"EnumProp\":\"Two\"}");
            Assert.Equal(TestEnum.Two, deserialized.EnumProp);
        }

        [Fact]
        public void DeserializesMissingDateTimeAsDefault()
        {
            var deserialized = GetDeserialized("{}");
            Assert.Equal(default, deserialized.DateTimeProp);
        }

        [Fact]
        public void DeserializesMissingDateTimeOffsetAsDefault()
        {
            var deserialized = GetDeserialized("{}");
            Assert.Equal(default, deserialized.DateTimeOffsetProp);
        }

        private DtoTestClass GetDeserialized(string json)
        {
            var jsonOptions = new JsonSerializerSettings();
            jsonOptions.ConfigureDefaultJsonSerializerSettings();
            return JsonConvert.DeserializeObject<DtoTestClass>(json, jsonOptions);
        }

        public class DtoTestClass
        {
            public TestEnum EnumProp { get; set; }
            public DateTime DateTimeProp { get; set; }
            public DateTimeOffset DateTimeOffsetProp { get; set; }
        }

        public enum TestEnum
        {
            One = 1,
            Two = 2
        }
    }
}
