// UserSessionService.cs

using WebAPI.Entities;
using WebAPI.Enums;

namespace WebAPI.Session;

public class UserSessionService
{
    public User? CurrentUser { get; set; }

    public bool IsLoggedIn => CurrentUser != null;

    public bool IsReviewer =>
        CurrentUser != null && CurrentUser.Role == Role.Reviewer;

    public bool IsPublisher =>
        CurrentUser != null && CurrentUser.Role == Role.Publisher;

    public void Logout()
    {
        CurrentUser = null;
    }
}