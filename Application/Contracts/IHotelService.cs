using Application.DTOs;

namespace Application.Contracts;

public interface IHotelService
{
    void CreateNewHotel(HotelDTO hotel);
}
