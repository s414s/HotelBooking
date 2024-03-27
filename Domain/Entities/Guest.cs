namespace Domain;
public class Guest : Entity
{
    public string Name { get; init; }
    public Guest(string name)
    {
        Id = new Guid();
        Name = name;
    }
}