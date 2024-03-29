using Application.DTOs;

namespace Application.Contracts;

public interface IHotelService
{
    IEnumerable<HotelDTO> GetAll();
}
