using Domain;

namespace Application.DTOs;

public class HotelDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Cities City { get; set; }
    public int NumberOfRooms { get; set; }
    public static HotelDTO MapFromDomainEntity(Hotel hotel)
    {
        return new HotelDTO
        {
            Id = hotel.Id,
            Name = hotel.Name,
            City = hotel.City,
            NumberOfRooms = hotel.Rooms.Count(),
        };
    }
    public override string ToString()
        => $"Name: {Name}, Location: {City}, Rooms: {NumberOfRooms}";
}
