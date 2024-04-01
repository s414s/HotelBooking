using System.Text.Json.Serialization;

namespace Domain;
public class User : Entity
{
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;    
    public string Password { get; set; } = string.Empty;
    [JsonIgnore]
    public string Username { get => $"{Name.ToLower()}{Surname.ToLower()}"; }

    public User() { }
    public User(string name, string surname, string password)
    {
        Id = Guid.NewGuid();
        Name = name.ToLower();
        Surname = surname.ToLower();
        Password = password;
    }
}