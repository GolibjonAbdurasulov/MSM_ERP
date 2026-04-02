using System.Text.Json;
using WebAPI.Entities;
using WebAPI.Interfaces;

namespace WebAPI.Services;

public class UserService : IUserService
{
    private readonly string _filePath = @"C:\Users\Developr\RiderProjects\MSM_ERP_Test\WebAPI\DateBase\users.json";
    // Fayldan hamma foydalanuvchilarni o'qish (Private Helper)
    public List<User> GetAllUsersFromFile()
    {
        if (!File.Exists(_filePath)) return new List<User>();
        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
    }

    // Ro'yxatni faylga saqlash (Private Helper)
    private void SaveAllUsersToFile(List<User> users)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(users, options);
        File.WriteAllText(_filePath, json);
    }

    public User CreateUser(User user)
    {
        var users = GetAllUsersFromFile();
        user.Id = users.Count > 0 ? users.Max(u => u.Id) + 1 : 1;
        users.Add(user);
        SaveAllUsersToFile(users);
        return user;
    }

    public User UpdateUser(User user)
    {
        var users = GetAllUsersFromFile();
        var existingUser = users.FirstOrDefault(u => u.Id == user.Id);
        if (existingUser != null)
        {
            existingUser.FullName = user.FullName;
            existingUser.Email = user.Email;
            existingUser.Password = user.Password;
            existingUser.Role=user.Role;
            SaveAllUsersToFile(users);
        }
        return existingUser;
    }

    public User DeleteUser(User user)
    {
        var users = GetAllUsersFromFile();
        var userToDelete = users.FirstOrDefault(u => u.Id == user.Id);
        if (userToDelete != null)
        {
            users.RemoveAll(u => u.Id == user.Id);
            SaveAllUsersToFile(users);
        }
        return userToDelete;
    }

    public User GetUserByEmail(string email)
    {
        return GetAllUsersFromFile().FirstOrDefault(u => u.Email == email);
    }

    public User GetUserById(long id)
    {
        return GetAllUsersFromFile().FirstOrDefault(u => u.Id == id);
    }
}
