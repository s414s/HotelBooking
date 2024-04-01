namespace Domain;
public class Guest : Entity
{
    public string Name { get; init; } = String.Empty;
    public Guest() { }
    public Guest(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }
}