using DataAccess.Implementations.Base;
using Domain;
using Domain.Contracts;

namespace DataAccess.Implementations;

public class RepositoryHotelsPersistent : RepositoryPersistent<Hotel>, IRepository<Hotel>
{
    public RepositoryHotelsPersistent() : base("hotelsStorage.json") { }
}
