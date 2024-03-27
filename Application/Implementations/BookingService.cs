using Application.Contracts;
using Application.DTOs;
using Domain;
using Domain.Contracts;

namespace Application.Implementations;

public class BookingService : IBookingService
{
    private readonly IRepository<Hotel> _hotelsRepo;
    private readonly IRepository<Room> _roomsRepo;
    private readonly IRepository<Booking> _bookingsRepo;

    public BookingService(
        IRepository<Hotel> hotelsRepo,
        IRepository<Room> roomsRepo,
        IRepository<Booking> bookingsRepo)
    {
        _hotelsRepo = hotelsRepo;
        _roomsRepo = roomsRepo;
        _bookingsRepo = bookingsRepo;
    }

    public void BookRoomForGuest(Guid roomId, IEnumerable<string> guestNames, DateOnly from, DateOnly until)
    {
        var room = _roomsRepo.GetByID(roomId) ?? throw new ApplicationException("the room does not exist");

        if (!room.IsAvailableBetweenDates(from, until))
            throw new ApplicationException("the room does not exist");

        room.Bookings = room.Bookings.Append(new Booking(from, until, guestNames));

        _roomsRepo.Update(room);
        _roomsRepo.SaveChanges();
    }

    public void DeleteBooking(Guid bookingId)
    {
        var booking = _bookingsRepo.GetByID(bookingId) ?? throw new ApplicationException("the booking does not exist");

        _bookingsRepo.Update(booking);
        _bookingsRepo.SaveChanges();
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
