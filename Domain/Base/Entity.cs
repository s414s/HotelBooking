using System.Text.Json.Serialization;

namespace Domain;

public class Entity
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    public bool Equal(Entity other)
    {
        return other.Id == Id;
    }
}
