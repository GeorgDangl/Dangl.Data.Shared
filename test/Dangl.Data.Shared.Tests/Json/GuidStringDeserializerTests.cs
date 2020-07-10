using Dangl.Data.Shared.Json;
using Newtonsoft.Json;
using System;
using Xunit;

namespace Dangl.Data.Shared.Tests.Json
{
    public class GuidStringDeserializerTests
    {
        [Fact]
        public void CanDeserializeNullGuidProperty()
        {
            var input = "{\"id\": null}";
            var actual = DeserializeTestClass(input);
            Assert.Equal(Guid.Empty, actual.Id);
        }

        [Fact]
        public void CanDeserializeEmptyGuidProperty()
        {
            var input = "{\"id\": \"\"}";
            var actual = DeserializeTestClass(input);
            Assert.Equal(Guid.Empty, actual.Id);
        }

        [Fact]
        public void CanDeserializeRegularGuidProperty()
        {
            var input = "{\"id\": \"ef43c2ea-8a24-4ff6-b29f-0e2d418f4ece\"}";
            var actual = DeserializeTestClass(input);
            Assert.Equal(new Guid("ef43c2ea-8a24-4ff6-b29f-0e2d418f4ece"), actual.Id);
        }

        public class TestClass
        {
            public Guid Id { get; set; }
        }

        private TestClass DeserializeTestClass(string json)
        {
            var jsonOptions = new JsonSerializerSettings();
            jsonOptions.Converters.Add(new GuidStringDeserializer());
            var deserialized = JsonConvert.DeserializeObject<TestClass>(json, jsonOptions);
            return deserialized;
        }
    }
}
