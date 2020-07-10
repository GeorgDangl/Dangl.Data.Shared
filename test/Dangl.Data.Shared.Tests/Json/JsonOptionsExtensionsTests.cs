using Dangl.Data.Shared.Json;
using Newtonsoft.Json;
using System;
using Xunit;

namespace Dangl.Data.Shared.Tests.Json
{
    public class JsonOptionsExtensionsTests
    {
        [Fact]
        public void SerializedWithDefaultCase()
        {
            var jsonOptions = new JsonSerializerSettings();
            jsonOptions.ConfigureDefaultJsonSerializerSettings(false);
            var input = new DtoTestClass { EnumProp = TestEnum.Two };
            var serialized = JsonConvert.SerializeObject(input, jsonOptions);
            Assert.Contains("\"EnumProp\"", serialized);

        }

        [Fact]
        public void SerializesWithCamelCase()
        {
            var jsonOptions = new JsonSerializerSettings();
            jsonOptions.ConfigureDefaultJsonSerializerSettings(true);
            var input = new DtoTestClass { EnumProp = TestEnum.Two };
            var serialized = JsonConvert.SerializeObject(input, jsonOptions);
            Assert.Contains("\"enumProp\"", serialized);
        }

        [Fact]
        public void SerializesEnumAsString_01()
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

        [Fact]
        public void DoesSerializeDefaultEnum()
        {
            var dto = new JsonTestDto();
            var serialized = Serialize(dto);
            Assert.Contains($"\"{nameof(JsonTestDto.EnumValue)}\"", serialized);
        }

        [Fact]
        public void SerializesEnumAsString_02()
        {
            var dto = new JsonTestDto();
            dto.EnumValue = JsonTestEnum.NonDefault;
            var serialized = Serialize(dto);
            Assert.Contains($"\"{JsonTestEnum.NonDefault.ToString()}\"", serialized);
        }

        [Fact]
        public void DoesNotSerializeNullValue()
        {
            var dto = new JsonTestDto();
            dto.DateTimeOffsetProperty = DateTimeOffset.UtcNow;
            var serialized = Serialize(dto);
            Assert.DoesNotContain($"\"{nameof(JsonTestDto.InnerDto)}\"", serialized);
        }

        [Fact]
        public void SerializesComplexProperty()
        {
            var dto = new JsonTestDto();
            dto.InnerDto = new JsonTestDto();
            var serialized = Serialize(dto);
            Assert.Contains($"\"{nameof(JsonTestDto.InnerDto)}\"", serialized);
        }

        [Fact]
        public void DoesNotSerializeDateTimeMin()
        {
            var dto = new JsonTestDto();
            dto.DateTimeOffsetProperty = DateTimeOffset.UtcNow;
            var serialized = Serialize(dto);
            Assert.DoesNotContain($"\"{nameof(JsonTestDto.DateTimeProperty)}\"", serialized);
        }

        [Fact]
        public void SerializesDateTime()
        {
            var dto = new JsonTestDto();
            dto.DateTimeProperty = DateTime.UtcNow;
            var serialized = Serialize(dto);
            // The "\":\"" part ensures that a string values is serialized, null would have no surrounding quotes
            Assert.Contains(nameof(JsonTestDto.DateTimeProperty) + "\":\"", serialized);
        }

        [Fact]
        public void DoesNotSerializeDateTimeOffsetMin()
        {
            var dto = new JsonTestDto();
            dto.DateTimeProperty = DateTime.UtcNow;
            var serialized = Serialize(dto);
            Assert.DoesNotContain($"\"{nameof(JsonTestDto.DateTimeOffsetProperty)}\"", serialized);
        }

        [Fact]
        public void SerializesDateTimeOffset()
        {
            var dto = new JsonTestDto();
            dto.DateTimeOffsetProperty = DateTimeOffset.UtcNow;
            var serialized = Serialize(dto);
            // The "\":\"" part ensures that a string values is serialized, null would have no surrounding quotes
            Assert.Contains(nameof(JsonTestDto.DateTimeOffsetProperty) + "\":\"", serialized);
        }

        [Fact]
        public void DeserializesNullAsDefaultDateTime()
        {
            var input = "{\"DateTimeProperty\": null}";
            var deserialized = Deserialize(input);
            Assert.Equal(default, deserialized.DateTimeProperty);
        }

        [Fact]
        public void DeserializesNullAsdDefaultDateTimeOffset()
        {
            var input = "{\"DateTimeOffsetProperty\": null}";
            var deserialized = Deserialize(input);
            Assert.Equal(default, deserialized.DateTimeOffsetProperty);
        }

        private JsonTestDto Deserialize(string jsonString)
        {
            var deserialized = JsonConvert.DeserializeObject<JsonTestDto>(jsonString, GetSerializerSettings());
            return deserialized;
        }

        private string Serialize(JsonTestDto dto)
        {
            var serialized = JsonConvert.SerializeObject(dto, GetSerializerSettings());
            return serialized;
        }

        private JsonSerializerSettings GetSerializerSettings()
        {
            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ConfigureDefaultJsonSerializerSettings();
            return serializerSettings;
        }

        public class JsonTestDto
        {
            public DateTime DateTimeProperty { get; set; }
            public DateTimeOffset DateTimeOffsetProperty { get; set; }
            public JsonTestDto InnerDto { get; set; }
            public JsonTestEnum EnumValue { get; set; }
        }

        public enum JsonTestEnum
        {
            Default = 0,
            NonDefault = 1
        }
    }

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
