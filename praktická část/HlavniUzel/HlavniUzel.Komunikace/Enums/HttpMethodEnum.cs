using System.Text.Json.Serialization;

namespace HlavniUzel.Komunikace.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum HttpMethodEnum { GET, POST }
}
