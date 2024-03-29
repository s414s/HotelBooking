namespace Domain;
public class Hotel : Entity
{
    public string Name { get; set; } = String.Empty;
    public Address? Address { get; set; }
    public IEnumerable<Room> Rooms { get; set; } = [];
    public IEnumerable<User> Users { get; set; } = [];

    public Hotel() { }
    public Hotel(string name, Address address)
    {
        Id = new Guid();
        Name = name;
        Address = address;
    }

    public IEnumerable<Room> GetAvailableRoomsBetweenDates(DateOnly start, DateOnly end)
        => Rooms.Where(x => x.IsAvailableBetweenDates(start, end));

    public int GetOccupationRatioOnDate(DateOnly date)
        => Rooms.Where(x => !x.IsAvailableBetweenDates(date, date)).Count() / Rooms.Count();
}

