using Application.DTOs;

namespace Application.Contracts;

public interface IBookingService
{
    void BookRoomForGuest(Guid roomId, IEnumerable<string> guestNames, DateOnly from, DateOnly until);
    void DeleteBooking(Guid bookingId);
    IEnumerable<BookingDTO> GetBookingsOfMonth(int month, int year, Guid? hotelId);
}
