using System.Text.Json.Serialization;

namespace Domain;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RoomTypes
{
    Single,
    Double,
    Suite
}
