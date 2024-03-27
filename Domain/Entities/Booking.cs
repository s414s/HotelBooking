namespace Domain;
public class Booking : Entity
{
    public DateOnly Start { get; init; }
    public DateOnly End { get; init; }
    public IEnumerable<Guest> Guests { get; init; }
    public Booking(DateOnly start, DateOnly end, IEnumerable<Guest> guests)
    {
        Start = start;
        End = end;
        Guests = guests;
    }
}