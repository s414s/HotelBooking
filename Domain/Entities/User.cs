using System.Text.Json.Serialization;

namespace Domain;
public class User : Entity
{
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;    
    public string Password { get; set; } = string.Empty;
    //public Roles Role { get; set; }
    [JsonIgnore]
    public string Username { get => $"{Name.ToLower()}{Surname.ToLower()}"; }

    public User() { }
    public User(string name, string surname, string password)
    {
        Id = new Guid();
        Name = name;
        Surname = surname;
        Password = password;
        //Role = role;
    }
}