using Microsoft.EntityFrameworkCore;
using Server.Interfaces;
using SharedProject.Models;

namespace Server.Repositories;

public class UserRepository : IUserRepository
{
    private readonly Context _context;

    public UserRepository(Context context)
    {
        _context = context;
    }

    public async Task<User?> Get(string username)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
        return user;
    }

    public async Task<bool> Save(User user)
    {
        if (user == null)
            return false;

        var entity = await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return true;
    }
}
