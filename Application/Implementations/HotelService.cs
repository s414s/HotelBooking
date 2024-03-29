using Application.Contracts;
using Application.DTOs;
using Domain;
using Domain.Contracts;

namespace Application.Implementations;

public class HotelService : IHotelService
{
    private readonly IRepository<Hotel> _hotelsRepo;

    public HotelService(IRepository<Hotel> hotelsRepo)
    {
        _hotelsRepo = hotelsRepo;
    }

    public IEnumerable<HotelDTO> GetAll()
        => _hotelsRepo.GetAll().Select(x => HotelDTO.MapFromDomainEntity(x));

}
