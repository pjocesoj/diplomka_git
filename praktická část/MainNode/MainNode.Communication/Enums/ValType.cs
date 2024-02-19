using System.Text.Json.Serialization;

namespace MainNode.Communication.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ValType { INT, BOOL, FLOAT }
}
