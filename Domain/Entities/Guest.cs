namespace Domain;
public class Guest
{
    public string Name { get; init; } = String.Empty;
    public Guest() { }
    public Guest(string name)
    {
        Name = name;
    }
}