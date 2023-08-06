using BlazorApp_Client.Models;

namespace BlazorApp_Client.Service;

public interface IUserService
{
    Task<List<User>> GetAllUsers();
    Task<bool> SaveUser(User user);
}