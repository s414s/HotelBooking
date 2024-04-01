using System.Text.Json.Serialization;

namespace Domain;
public class Room : Entity
{
    public int Storey { get; set; }
    public RoomTypes Type { get; set; }
    public IEnumerable<Booking> Bookings { get; set; } = [];
    [JsonIgnore]
    public int Capacity { get => Type == RoomTypes.Single ? 1 : 2; }

    public Room() { }
    public Room(int storey, RoomTypes type = RoomTypes.Single)
    {
        Id = Guid.NewGuid();
        Storey = storey;
        Type = type;
    }

    public bool IsAvailableBetweenDates(DateOnly start, DateOnly end)
        => !Bookings.Any(x => x.Start >= start && x.End <= end);

    public void Book(DateOnly start, DateOnly end, IEnumerable<Guest> guests)
    {
        if (!IsAvailableBetweenDates(start, end))
            throw new ApplicationException("room is already booked");

        if (guests.Count() > Capacity)
            throw new ApplicationException("room is not big enough");

        if (!guests.Any())
            throw new ApplicationException("no guest inserted");

        Bookings = Bookings.Append(new Booking(start, end, guests));
    }
}