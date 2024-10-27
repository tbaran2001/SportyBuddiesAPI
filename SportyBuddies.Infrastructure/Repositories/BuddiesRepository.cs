using Microsoft.EntityFrameworkCore;
using SportyBuddies.Domain.Buddies;

namespace SportyBuddies.Infrastructure.Repositories;

public class BuddiesRepository(SportyBuddiesDbContext dbContext) : IBuddiesRepository
{
    public async Task<IEnumerable<Buddy>> GetUserBuddiesAsync(Guid userId, bool includeUsers)
    {
        var query = dbContext.Buddies
            .Where(b => b.UserId == userId);

        if (includeUsers)
            query = query
                .Include(b => b.User)
                .Include(b => b.MatchedUser);

        return includeUsers
            ? await query.ToListAsync()
            : await query.Select(b => Buddy.Create(b.User, b.MatchedUser, b.MatchDateTime))
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
}