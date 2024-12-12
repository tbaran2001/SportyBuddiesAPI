using Microsoft.EntityFrameworkCore;
using SportyBuddies.Domain.Buddies;
using SportyBuddies.Domain.Common.Interfaces.Repositories;

namespace SportyBuddies.Infrastructure.Repositories;

public class BuddiesRepository(SportyBuddiesDbContext dbContext) : IBuddiesRepository
{
    public async Task<IEnumerable<Buddy>> GetProfileBuddiesAsync(Guid profileId)
    {
        return await dbContext.Buddies
            .Include(b=>b.MatchedProfile)
            .Include(b=>b.Conversation)
            .Where(b => b.ProfileId == profileId)
            .ToListAsync();
    }

    public async Task AddBuddyAsync(Buddy buddy)
    {
        await dbContext.Buddies.AddAsync(buddy);
    }

    public async Task<bool> AreProfilesAlreadyBuddiesAsync(Guid profileId, Guid matchedProfileId)
    {
        return await dbContext.Buddies
            .AnyAsync(b => b.ProfileId == profileId && b.MatchedProfileId == matchedProfileId);
    }

    public async Task RemoveProfileBuddiesAsync(Guid profileId)
    {
        await dbContext.Buddies
            .Where(b => b.ProfileId == profileId || b.MatchedProfileId == profileId)
            .ExecuteDeleteAsync();
    }

    public async Task<IEnumerable<Buddy>> GetRelatedBuddies(Guid profileId, Guid matchedProfileId)
    {
        return await dbContext.Buddies
            .Include(b=>b.Conversation)
            .Where(b => (b.ProfileId == profileId && b.MatchedProfileId == matchedProfileId) || (b.ProfileId == matchedProfileId && b.MatchedProfileId == profileId))
            .ToListAsync();
    }
}