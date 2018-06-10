using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Dangl.Data.Shared.AspNetCore.Json
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
            jsonSerializerSettings.Converters.Add(new StringEnumConverter());
            jsonSerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            jsonSerializerSettings.ContractResolver = new DefaultValuesContractResolver();
        }
    }
}
