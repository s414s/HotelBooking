using Application.DTOs;

namespace Application.Contracts;

public interface IRoomService
{
    IEnumerable<RoomDTO> GetAvailableRoomsOnDate(DateOnly from, DateOnly until, Guid? hotelId);
    void AddNewRoom(RoomDTO newRoom, Guid hotelId);
    void DeleteRoom(RoomDTO room);
}
