using Microsoft.EntityFrameworkCore;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Domain.Users;
using SportyBuddies.Infrastructure.Common.Persistence;

namespace SportyBuddies.Infrastructure.Users.Persistence;

public class UsersRepository(SportyBuddiesDbContext dbContext) : IUsersRepository
{
    public async Task<User?> GetUserByIdAsync(Guid userId)
    {
        return await dbContext.Users.FindAsync(userId);
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await dbContext.Users.ToListAsync();
    }

    public async Task AddUserAsync(User user)
    {
        await dbContext.Users.AddAsync(user);
    }

    public void RemoveUser(User user)
    {
        dbContext.Users.Remove(user);
    }

    public async Task<User?> GetUserByIdWithSportsAsync(Guid userId)
    {
        return await dbContext.Users
            .Include(u => u.Sports)
            .FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task<IEnumerable<User>> GetAllUsersWithSportsAsync()
    {
        return await dbContext.Users
            .Include(u => u.Sports)
            .ToListAsync();
    }
}