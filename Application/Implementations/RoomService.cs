using Application.Contracts;
using Application.DTOs;
using Domain;
using Domain.Contracts;

namespace Application.Implementations;

public class RoomService : IRoomService
{
    private readonly IRepository<Hotel> _hotelsRepo;

    public RoomService(IRepository<Hotel> hotelsRepo)
    {
        _hotelsRepo = hotelsRepo;
    }

    public void AddNewRoom(RoomDTO newRoom, Guid hotelId)
    {
        var hotel = _hotelsRepo.GetByID(hotelId) ?? throw new ApplicationException("the hotel does not exist");
        hotel.Rooms = hotel.Rooms.Append(newRoom.MapToDomainEntity()).ToList();
        _hotelsRepo.SaveChanges();
    }

    public void DeleteRoom(RoomDTO room)
    {
        var hotel = _hotelsRepo
            .GetAll()
            .FirstOrDefault(x => x.Rooms.Any(x => x.Id == room.Id))
            ?? throw new ApplicationException("the room does not exist");

        hotel.Rooms = hotel.Rooms.Where(x => x.Id != room.Id).ToList();
        _hotelsRepo.SaveChanges();
    }

    public IEnumerable<RoomDTO> GetAvailableRoomsOnDate(DateOnly from, DateOnly until, Guid? hotelId)
    {
        return _hotelsRepo.GetAll()
            .Where(x => hotelId == null || x.Id == hotelId)
            .SelectMany(x => x.Rooms)
            .Where(x => x.IsAvailableBetweenDates(from, until))
            .Select(x => RoomDTO.MapFromDomainEntity(x));
    }
}
