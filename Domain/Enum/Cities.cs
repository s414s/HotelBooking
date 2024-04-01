using System.Text.Json.Serialization;

namespace Domain;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Cities
{
    Madrid,
    Barcelona,
    Zaragoza
}
