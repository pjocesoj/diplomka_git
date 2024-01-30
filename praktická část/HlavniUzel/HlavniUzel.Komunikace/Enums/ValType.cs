using System.Text.Json.Serialization;

namespace HlavniUzel.Komunikace.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ValType { INT, BOOL, FLOAT }
}
