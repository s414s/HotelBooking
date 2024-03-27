using System.Text.Json.Serialization;

namespace Domain;
public class User : Entity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Password { get; set; }
    public Roles Role { get; set; }
    [JsonIgnore]
    public string Username { get => $"{Name.ToLower()}{Surname.ToLower()}"; }

    public User(string name, string surname, string password, Roles role = Roles.User)
    {
        Id = new Guid();
        Name = name;
        Surname = surname;
        Password = password;
        Role = role;
    }
}