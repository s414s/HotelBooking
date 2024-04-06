using Application.DTOs;

namespace Application.Contracts;

public interface IBookingService
{
    IEnumerable<BookingDTO> GetAll();
    void BookRoom(Guid roomId, IEnumerable<string> guestNames, DateOnly from, DateOnly until);
    void DeleteBooking(Guid bookingId);
    IEnumerable<BookingDTO> GetBookingsOfMonth(int month, int year, Guid? hotelId);
    IEnumerable<BookingDTO> GetFilteredBookings(string hotelName);
}
