using System.Text.Json.Serialization;

namespace MainNode.Communication.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum EndpointType
{
    GET,
    SET
}
