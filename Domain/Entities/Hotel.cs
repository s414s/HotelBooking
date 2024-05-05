namespace Domain;
public class Hotel : Entity
{
    public string Name { get; set; } = String.Empty;
    public string Address { get; set; } = String.Empty;
    public Cities City { get; set; }
    public IEnumerable<Room> Rooms { get; set; } = [];

    public Hotel() { }
    public Hotel(string name, string address, Cities city)
    {
        Id = Guid.NewGuid();
        Name = name.ToLower();
        Address = address;
        City = city;
    }

    public IEnumerable<Room> GetAvailableRoomsBetweenDates(DateOnly start, DateOnly end)
        => Rooms.Where(x => x.IsAvailableBetweenDates(start, end));

    public int GetOccupationRatioOnDate(DateOnly date)
        => Rooms.Where(x => !x.IsAvailableBetweenDates(date, date)).Count() / Rooms.Count();
}

