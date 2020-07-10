using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Dangl.Data.Shared.Json
{
    /// <summary>
    /// This will call the base method and, in case of a deserialization failure, check if the string given
    /// was null or empty and then return the enums default value.
    /// </summary>
    public class EmptyEnumDeserializer : StringEnumConverter
    {
        /// <summary>
        /// This will call the base method and, in case of a deserialization failure, check if the string given
        /// was null or empty and then return the enums default value.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            try
            {
                var baseResult = base.ReadJson(reader, objectType, existingValue, serializer);
                return baseResult;
            }
            catch (JsonSerializationException)
            {
                switch (reader.TokenType)
                {
                    case JsonToken.Null:
                    case JsonToken.String when string.IsNullOrWhiteSpace(reader.Value as string):
                        // Null or empty string should just return the default enum
                        return Activator.CreateInstance(objectType);

                    default:
                        throw;
                }
            }
        }
    }
}
