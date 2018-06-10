using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Reflection;

namespace Dangl.Data.Shared.AspNetCore.Json
{
    /// <summary>
    /// This contract resolver ignores default values for DateTime, DateTimeOffset and Guid when serializing
    /// </summary>
    public class DefaultValuesContractResolver : DefaultContractResolver
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
    }
}
