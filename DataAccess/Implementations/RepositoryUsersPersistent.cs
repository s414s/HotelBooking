using DataAccess.Implementations.Base;
using Domain;
using Domain.Contracts;

namespace DataAccess.Implementations;

public class RepositoryUsersPersistent : RepositoryPersistent<User>, IRepository<User>
{
    public RepositoryUsersPersistent() : base("usersStorage.json") { }

    public User? GetByUsername(string username)
        => GetAll().FirstOrDefault(x => x.Username == username);
}
