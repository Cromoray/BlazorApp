using SharedProject.Models;

namespace Server.Interfaces;

public interface IUserRepository
{
    Task<User?> Get(string username);
    Task<bool> Save(User user);
}