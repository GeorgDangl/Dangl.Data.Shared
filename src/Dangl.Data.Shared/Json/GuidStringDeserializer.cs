using Newtonsoft.Json;
using System;

namespace Dangl.Data.Shared.Json
{
    /// <summary>
    /// If the value is null or an empty string, it returns an empty Guid, otherwise it tries to instantiate
    /// a new Guid or throw a <see cref="JsonSerializationException"/> when the Json token is of an unexpected type
    /// </summary>
    public class GuidStringDeserializer : JsonConverter
    {
        /// <summary>
        /// Returns always true
        /// </summary>
        public override bool CanRead => true;

        /// <summary>
        /// Returns always false
        /// </summary>
        public override bool CanWrite => false;

        /// <summary>
        /// Can convert only <see cref="Guid"/>s
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Guid);
        }

        /// <summary>
        /// If the value is null or an empty string, it returns an empty Guid, otherwise it tries to instantiate
        /// a new Guid or throw a <see cref="JsonSerializationException"/> when the Json token is of an unexpected type
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.Null:
                    return Guid.Empty;

                case JsonToken.String:
                    string str = reader.Value as string;
                    if (string.IsNullOrEmpty(str))
                    {
                        return Guid.Empty;
                    }
                    else
                    {
                        return new Guid(str);
                    }

                default:
                    throw new JsonSerializationException("Invalid token type for Guid deserialization: " + reader.TokenType);
            }
        }

        /// <summary>
        /// This method is not implemented
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
