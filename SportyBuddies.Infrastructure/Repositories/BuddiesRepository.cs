using Microsoft.EntityFrameworkCore;
using SportyBuddies.Domain.Buddies;

namespace SportyBuddies.Infrastructure.Repositories;

public class BuddiesRepository(SportyBuddiesDbContext dbContext) : IBuddiesRepository
{
    public async Task<IEnumerable<Buddy>> GetUserBuddiesAsync(Guid userId)
    {
        return await dbContext.Buddies
            .Where(b => b.UserId == userId)
            .ToListAsync();
    }

    public async Task AddBuddyAsync(Buddy buddy)
    {
        await dbContext.Buddies.AddAsync(buddy);
    }

    public async Task<bool> AreUsersBuddiesAsync(Guid userId, Guid matchedUserId)
    {
        return await dbContext.Buddies
            .AnyAsync(b => b.UserId == userId && b.MatchedUserId == matchedUserId);
    }

    public async Task RemoveUserBuddiesAsync(Guid userId)
    {
        await dbContext.Buddies
            .Where(b => b.UserId == userId || b.MatchedUserId == userId)
            .ExecuteDeleteAsync();
    }
}