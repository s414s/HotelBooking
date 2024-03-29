using Domain;

namespace Application.DTOs;

public class HotelDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public int NumberOfRooms { get; set; }
    public static HotelDTO MapFromDomainEntity(Hotel hotel)
    {
        return new HotelDTO
        {
            Id = hotel.Id,
            Name = hotel.Name,
            City = hotel.Address?.City.ToString() ?? "no city found",
            NumberOfRooms = hotel.Rooms.Count(),
        };
    }
    public override string ToString()
        => $"Id: {Id}, Name: {Name}, Location: {City}, Rooms: {NumberOfRooms}";
}
