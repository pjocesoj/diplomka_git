using System.Text.Json.Serialization;

namespace MainNode.Communication.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum EndPointType
{
    EP_TYPE_GET,
    EP_TYPE_SET
}
