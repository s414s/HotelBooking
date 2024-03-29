using Application.Contracts;
using Application.DTOs;
using Domain;
using Domain.Contracts;

namespace Application.Implementations;

public class BookingService : IBookingService
{
    private readonly IRepository<Hotel> _hotelsRepo;

    public BookingService(IRepository<Hotel> hotelsRepo)
    {
        _hotelsRepo = hotelsRepo;
    }

    public void BookRoom(Guid roomId, IEnumerable<string> guestNames, DateOnly from, DateOnly until)
    {
        //var room = _roomsRepo.GetByID(roomId) ?? throw new ApplicationException("the room does not exist");
        var room = _hotelsRepo
            .GetAll()
            .SelectMany(x => x.Rooms)
            .FirstOrDefault(x => x.Id == roomId)
            ?? throw new ApplicationException("the room does not exist");

        if (!room.IsAvailableBetweenDates(from, until))
            throw new ApplicationException("the room is not available");

        room.Bookings = room.Bookings.Append(new Booking(from, until, guestNames.Select(x => new Guest(x)))).ToList();

        //_roomsRepo.Update(room);
        _hotelsRepo.SaveChanges();
    }

    public void DeleteBooking(Guid bookingId)
    {
        //var booking = _bookingsRepo.GetByID(bookingId) ?? throw new ApplicationException("the booking does not exist");
        var room = _hotelsRepo
            .GetAll()
            .SelectMany(x => x.Rooms)
            .FirstOrDefault(x => x.Bookings.Any(y => y.Id == bookingId))
            ?? throw new ApplicationException("the booking does not exist");

        room.Bookings = room.Bookings.Where(x => x.Id != bookingId).ToList();
        _hotelsRepo.SaveChanges();
    }

    public IEnumerable<BookingDTO> GetAll()
    {
        return _hotelsRepo
            .GetAll()
            .SelectMany(x => x.Rooms)
            .SelectMany(x => x.Bookings)
            .Select(x => BookingDTO.MapFromDomainEntity(x));
    }

    public IEnumerable<BookingDTO> GetBookingsOfMonth(int month, int year, Guid? hotelId)
    {
        return _hotelsRepo.GetAll()
            .Where(x => hotelId == null || x.Id == hotelId)
            .SelectMany(x => x.Rooms)
            .SelectMany(x => x.Bookings)
            .Where(x => (x.Start.Month == month && x.Start.Year == year)
                || (x.End.Month == month && x.End.Year == year))
            .Select(x => BookingDTO.MapFromDomainEntity(x));
    }
}
