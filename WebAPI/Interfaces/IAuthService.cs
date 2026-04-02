using WebAPI.Entities;

namespace WebAPI.Interfaces;

public interface IAuthService
{
    public User Login(string email, string password);
}