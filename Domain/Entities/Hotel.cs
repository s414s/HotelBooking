namespace Domain;
public class Hotel : Entity
{
    public string Name { get; set; }
    public Address Address { get; set; }
    public IEnumerable<Room> Rooms { get; set; }
    public IEnumerable<User> Users { get; set; }
    public Hotel(string name, Address address)
    {
        Id = new Guid();
        Name = name;
        Address = address;
        Rooms = [];
        Users = [];
    }

    public IEnumerable<Room> GetAvailableRoomsBetweenDates(DateOnly start, DateOnly end)
        => Rooms.Where(x => x.IsAvailableBetweenDates(start, end));

    public int GetOccupationRatioOnDate(DateOnly date)
        => Rooms.Where(x => !x.IsAvailableBetweenDates(date, date)).Count() / Rooms.Count();
}

