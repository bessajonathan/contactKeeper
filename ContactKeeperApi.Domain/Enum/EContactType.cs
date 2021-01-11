using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace ContactKeeperApi.Domain.Enum
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EContactType
    {
        [EnumMember(Value = "personal")]
        Personal,
        [EnumMember(Value = "commercial")]
        Commercial,
    }
}
