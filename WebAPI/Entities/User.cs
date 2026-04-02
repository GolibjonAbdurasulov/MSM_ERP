using WebAPI.Enums;

namespace WebAPI.Entities;

public class User
{
    public long  Id { get; set; }
    public string FullName { get; set; }
    public Role Role { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}