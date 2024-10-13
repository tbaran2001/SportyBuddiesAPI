using Microsoft.EntityFrameworkCore;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Domain.UserAggregate;
using SportyBuddies.Domain.UserAggregate.ValueObjects;
using SportyBuddies.Infrastructure.Common.Persistence;

namespace SportyBuddies.Infrastructure.Users.Persistence;

public class UsersRepository(SportyBuddiesDbContext dbContext) : IUsersRepository
{
    public async Task<User?> GetUserByIdAsync(UserId userId)
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

    public async Task<User?> GetUserByIdWithSportsAsync(UserId userId)
    {
        return await dbContext.Users
            .Include(u => u.SportIds)
            .FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task<IEnumerable<User>> GetAllUsersWithSportsAsync()
    {
        return await dbContext.Users
            .Include(u => u.SportIds)
            .ToListAsync();
    }
}