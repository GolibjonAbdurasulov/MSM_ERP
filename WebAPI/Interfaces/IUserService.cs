using WebAPI.Entities;

namespace WebAPI.Interfaces;

public interface IUserService
{
    public User CreateUser(User user);
    public User UpdateUser(User user);
    public User DeleteUser(User user);
    public User GetUserByEmail(string email);
    public User GetUserById(long id);
    public List<User> GetAllUsersFromFile();
}