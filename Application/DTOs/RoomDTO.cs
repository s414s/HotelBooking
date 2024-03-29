using Domain;

namespace Application.DTOs;

public class RoomDTO
{
    public Guid Id { get; set; }
    public int Storey { get; set; }
    public string Type { get; set; }
    public int Capacity { get; set; }

    public static RoomDTO MapFromDomainEntity(Room room)
    {
        return new RoomDTO
        {
            Id = room.Id,
            Storey = room.Storey,
            Type = room.Type.ToString(),
            Capacity = room.Capacity,
        };
    }

    public Room MapToDomainEntity() => new(Storey, Enum.Parse<RoomTypes>(Type, true));

    public override string ToString()
        => $"Id: {Id}, Storey: {Storey}, Type: {Type}, Capacity: {Capacity}";
}
