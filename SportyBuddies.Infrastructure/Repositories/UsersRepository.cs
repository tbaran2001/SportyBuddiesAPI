using Microsoft.EntityFrameworkCore;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Infrastructure.Repositories;

public class UsersRepository(SportyBuddiesDbContext dbContext) : IUsersRepository
{
    public async Task<User?> GetUserByIdAsync(Guid userId)
    {
        return await dbContext.Users.FindAsync(userId);
    }

    public async Task<User?> GetUserByIdWithSportsAsync(Guid userId)
    {
        return await dbContext.Users
            .Include(u => u.Sports)
            .FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task<User?> GetUserByIdWithPhotosAsync(Guid userId)
    {
        return await dbContext.Users
            .Include(u => u.MainPhoto)
            .Include(u => u.Photos)
            .FirstOrDefaultAsync(u => u.Id == userId);
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

    public async Task<IEnumerable<User>> GetAllUsersWithSportsAsync()
    {
        return await dbContext.Users
            .Include(u => u.Sports)
            .ToListAsync();
    }

    public async Task<bool> UserExistsAsync(Guid userId)
    {
        return await dbContext.Users.AnyAsync(u => u.Id == userId);
    }
}