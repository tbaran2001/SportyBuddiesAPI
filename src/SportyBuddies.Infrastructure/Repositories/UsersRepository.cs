using Microsoft.EntityFrameworkCore;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
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

    public async Task<IEnumerable<User>> GetPotentialMatchesAsync(Guid userId, IEnumerable<Guid> userSports)
    {
        return await dbContext.Users
            .Where(u => u.Id != userId)
            .Where(u => u.Sports.Any(s => userSports.Contains(s.Id)))
            .Where(u => !dbContext.Matches.Any(m =>
                (m.UserId == userId && m.MatchedUserId == u.Id) ||
                (m.UserId == u.Id && m.MatchedUserId == userId)))
            .ToListAsync();
    }

    public async Task AddPhotoAsync(UserPhoto userPhoto)
    {
        await dbContext.UserPhotos.AddAsync(userPhoto);
    }

    public void DeletePhotoAsync(UserPhoto userPhoto)
    {
        dbContext.UserPhotos.Remove(userPhoto);
    }
}