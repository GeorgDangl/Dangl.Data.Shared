using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Reflection;

namespace Dangl.Data.Shared.Json
{
    /// <summary>
    /// This contract resolver ignores default values for DateTime, DateTimeOffset and Guid when serializing. Additionally, it preserves
    /// the exact casing in keys for dictionaries.
    /// </summary>
    public class CamelCaseDefaultValuesContractResolver : CamelCasePropertyNamesContractResolver
    {
        /// <summary>
        /// This specifies to ignore default values for DateTime, DateTimeOffset
        /// and Guid
        /// </summary>
        /// <param name="member"></param>
        /// <param name="memberSerialization"></param>
        /// <returns></returns>
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty prop = base.CreateProperty(member, memberSerialization);

            if (prop.PropertyType == typeof(DateTime))
            {
                prop.DefaultValueHandling = DefaultValueHandling.Ignore;
            }

            if (prop.PropertyType == typeof(DateTimeOffset))
            {
                prop.DefaultValueHandling = DefaultValueHandling.Ignore;
            }

            if (prop.PropertyType == typeof(Guid))
            {
                prop.DefaultValueHandling = DefaultValueHandling.Ignore;
            }

            return prop;
        }

        /// <summary>
        /// This just returns the original key, no transformation is applied.
        /// </summary>
        /// <param name="dictionaryKey"></param>
        /// <returns></returns>
        protected override string ResolveDictionaryKey(string dictionaryKey)
        {
            // There should be no transofmration for dictionary keys
            return dictionaryKey;
        }
    }
}
