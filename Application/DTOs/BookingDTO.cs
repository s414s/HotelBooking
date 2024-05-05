using Domain;

namespace Application.DTOs;

public class BookingDTO
{
    public Guid Id { get; set; }
    public DateOnly From { get; set; }
    public DateOnly Until { get; set; }
    public IEnumerable<string> GuestNames { get; set; } = [];
    public string HotelName { get; set; } = string.Empty;
    public static BookingDTO MapFromDomainEntity(Booking booking)
    {
        return new BookingDTO
        {
            Id = booking.Id,
            From = booking.Start,
            Until = booking.End,
            GuestNames = booking.Guests.Select(g => g.Name),
            HotelName = booking.HotelName,
        };
    }

    public override string ToString()
        => $"From: {From}, To: {Until}, Guests: {string.Join(", ", GuestNames)}, Hotel: {HotelName}";
}
