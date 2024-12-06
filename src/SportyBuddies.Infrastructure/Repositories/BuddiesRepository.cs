using Microsoft.EntityFrameworkCore;
using SportyBuddies.Domain.Buddies;
using SportyBuddies.Domain.Common.Interfaces.Repositories;

namespace SportyBuddies.Infrastructure.Repositories;

public class BuddiesRepository(SportyBuddiesDbContext dbContext) : IBuddiesRepository
{
    public async Task<IEnumerable<Buddy>> GetUserBuddiesAsync(Guid userId)
    {
        return await dbContext.Buddies
            .Include(b=>b.MatchedUser)
            .Include(b=>b.Conversation)
            .Where(b => b.UserId == userId)
            .ToListAsync();
    }

    public async Task AddBuddyAsync(Buddy buddy)
    {
        await dbContext.Buddies.AddAsync(buddy);
    }

    public async Task<bool> AreUsersAlreadyBuddiesAsync(Guid userId, Guid matchedUserId)
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

    public async Task<IEnumerable<Buddy>> GetRelatedBuddies(Guid userId, Guid matchedUserId)
    {
        return await dbContext.Buddies
            .Include(b=>b.Conversation)
            .Where(b => (b.UserId == userId && b.MatchedUserId == matchedUserId) || (b.UserId == matchedUserId && b.MatchedUserId == userId))
            .ToListAsync();
    }
}