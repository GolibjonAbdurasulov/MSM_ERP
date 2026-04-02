using WebAPI.Entities;
using WebAPI.Interfaces;

namespace WebAPI.Auth;

public class AuthService : IAuthService
{
    private readonly IUserService _userService;

    public AuthService(IUserService userService)
    {
        _userService = userService;
    }

    public User Login(string email, string password)
    {
        var res=_userService.GetUserByEmail(email);
        if (res == null)
            throw new NullReferenceException();
        
        if (res.Password == password)
            return res;
        
        throw new NullReferenceException();
    }
}