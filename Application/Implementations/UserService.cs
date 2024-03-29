using Application.Contracts;
using Application.DTOs;
using Domain;
using Domain.Contracts;

namespace Application.Implementations;

public class UserService : IUserService
{
    private readonly IRepository<User> _usersRepo;

    public UserService(IRepository<User> userRepo)
    {
        _usersRepo = userRepo;
    }

    public void Create(UserDTO newUser)
    {
        _usersRepo.Add(newUser.MapToDomainEntity());
        _usersRepo.SaveChanges();
    }

    public void Delete(UserDTO user)
    {
        _usersRepo.Delete(user.MapToDomainEntity());
        _usersRepo.SaveChanges();
    }

    public UserDTO? SignIn(string username, string password)
    {
        var user = _usersRepo
            .GetAll()
            .FirstOrDefault(x => x.Username == username);

        return user?.Password != password
            ? null
            : UserDTO.MapFromDomainEntity(user);
    }
}
