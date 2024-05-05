namespace Domain;
public class Booking : Entity
{
    public DateOnly Start { get; init; }
    public DateOnly End { get; init; }
    public IEnumerable<Guest> Guests { get; init; } = [];
    public string HotelName { get; init; }
    public Booking() { }
    public Booking(DateOnly start, DateOnly end, IEnumerable<Guest> guests, string hotelName)
    {
        Id = Guid.NewGuid();
        Start = start;
        End = end;
        Guests = guests;
        HotelName = hotelName;
    }
}