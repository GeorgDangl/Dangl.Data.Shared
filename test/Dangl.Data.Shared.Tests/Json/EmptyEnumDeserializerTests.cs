using Dangl.Data.Shared.Json;
using Newtonsoft.Json;
using Xunit;

namespace Dangl.Data.Shared.Tests.Json
{
    public class EmptyEnumDeserializerTests
    {
        [Fact]
        public void CanDeserializeNullEnumProperty()
        {
            var input = "{\"value\": null}";
            var actual = DeserializeTestClass(input);
            Assert.Equal(default, actual.Value);
        }

        [Fact]
        public void CanDeserializeEmptyEnumProperty()
        {
            var input = "{\"value\": \"\"}";
            var actual = DeserializeTestClass(input);
            Assert.Equal(default, actual.Value);
        }

        [Fact]
        public void CanDeserializeRegularEnumProperty()
        {
            var input = "{\"value\": \"Second\"}";
            var actual = DeserializeTestClass(input);
            Assert.Equal(MyEnum.Second, actual.Value);
        }

        public class TestClass
        {
            public MyEnum Value { get; set; }
        }

        private TestClass DeserializeTestClass(string json)
        {
            var jsonOptions = new JsonSerializerSettings();
            jsonOptions.Converters.Add(new EmptyEnumDeserializer());
            var deserialized = JsonConvert.DeserializeObject<TestClass>(json, jsonOptions);
            return deserialized;
        }

        public enum MyEnum
        {
            First,
            Second
        }
    }
}
