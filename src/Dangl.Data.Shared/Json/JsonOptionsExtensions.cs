using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Dangl.Data.Shared.Json
{
    /// <summary>
    /// Extensions to configure Json serialization options
    /// </summary>
    public static class JsonOptionsExtensions
    {
        /// <summary>
        /// This enables the <see cref="StringEnumConverter"/>, sets <see cref="NullValueHandling"/> to ignore,
        /// and adds the <see cref="DefaultValuesContractResolver"/> to ignore default values for certain types, e.g.
        /// Guid.Empty, DateTime.MinValue and DateTimeOffset.MinValue
        /// </summary>
        /// <param name="jsonSerializerSettings"></param>
        public static void ConfigureDefaultJsonSerializerSettings(this JsonSerializerSettings jsonSerializerSettings)
        {
            jsonSerializerSettings.ConfigureDefaultJsonSerializerSettings(false);
        }

        /// <summary>
        /// This enables the <see cref="EmptyEnumDeserializer"/> and the <see cref="GuidStringDeserializer"/>, sets <see cref="NullValueHandling"/> to ignore,
        /// and adds the <see cref="DefaultValuesContractResolver"/> to ignore default values for certain types, e.g.
        /// Guid.Empty, DateTime.MinValue and DateTimeOffset.MinValue
        /// </summary>
        /// <param name="jsonSerializerSettings"></param>
        /// <param name="useCamelCaseContractResolver">If this is set to true, property names will be serialized using CamelCase</param>
        public static void ConfigureDefaultJsonSerializerSettings(this JsonSerializerSettings jsonSerializerSettings, bool useCamelCaseContractResolver)
        {
            jsonSerializerSettings.Converters.Add(new GuidStringDeserializer());
            jsonSerializerSettings.Converters.Add(new EmptyEnumDeserializer());
            jsonSerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            if (useCamelCaseContractResolver)
            {
                jsonSerializerSettings.ContractResolver = new CamelCaseDefaultValuesContractResolver();
            }
            else
            {
                jsonSerializerSettings.ContractResolver = new DefaultValuesContractResolver();
            }
        }
    }
}
