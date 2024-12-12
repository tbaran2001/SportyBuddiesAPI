using Microsoft.EntityFrameworkCore;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Profiles;

namespace SportyBuddies.Infrastructure.Repositories;

public class ProfilesRepository(SportyBuddiesDbContext dbContext) : IProfilesRepository
{
    public async Task<Profile?> GetProfileByIdAsync(Guid profileId)
    {
        return await dbContext.Profiles.FindAsync(profileId);
    }

    public async Task<Profile?> GetProfileByIdWithSportsAsync(Guid profileId)
    {
        return await dbContext.Profiles
            .Include(u => u.Sports)
            .FirstOrDefaultAsync(u => u.Id == profileId);
    }

    public async Task<IEnumerable<Profile>> GetAllProfilesAsync()
    {
        return await dbContext.Profiles.ToListAsync();
    }

    public async Task AddProfileAsync(Profile profile)
    {
        await dbContext.Profiles.AddAsync(profile);
    }

    public void RemoveProfile(Profile profile)
    {
        dbContext.Profiles.Remove(profile);
    }

    public async Task<IEnumerable<Profile>> GetPotentialMatchesAsync(Guid profileId, IEnumerable<Guid> userSports)
    {
        return await dbContext.Profiles
            .Where(u => u.Id != profileId)
            .Where(u => u.Sports.Any(s => userSports.Contains(s.Id)))
            .Where(u => !dbContext.Matches.Any(m =>
                (m.ProfileId == profileId && m.MatchedProfileId == u.Id) ||
                (m.ProfileId == u.Id && m.MatchedProfileId == profileId)))
            .ToListAsync();
    }
}