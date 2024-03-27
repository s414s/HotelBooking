using Application.DTOs;

namespace Application.Contracts;

public interface IUserService
{
    void Create(UserDTO newUser);
    UserDTO Update(UserDTO user);
    void Delete(UserDTO user);
    UserDTO? SignIn(string username, string password);
}
