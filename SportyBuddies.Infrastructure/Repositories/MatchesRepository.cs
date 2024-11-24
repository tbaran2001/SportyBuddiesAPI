using Microsoft.EntityFrameworkCore;
using SportyBuddies.Domain.Matches;

namespace SportyBuddies.Infrastructure.Repositories;

public class MatchesRepository(SportyBuddiesDbContext dbContext) : IMatchesRepository
{
    public async Task<Match?> GetMatchByIdAsync(Guid matchId)
    {
        return await dbContext.Matches.FindAsync(matchId);
    }

    public async Task<IEnumerable<Match>> GetUserMatchesAsync(Guid userId)
    {
        return await dbContext.Matches
            .Where(m => m.User.Id == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Match>> GetUserExistingMatchesAsync(Guid userId)
    {
        return await dbContext.Matches
            .Where(m => m.User.Id == userId || m.MatchedUser.Id == userId)
            .ToListAsync();
    }

    public async Task AddMatchesAsync(IEnumerable<Match> matches)
    {
        await dbContext.Matches.AddRangeAsync(matches);
    }

    public void RemoveMatches(IEnumerable<Match> matches)
    {
        dbContext.Matches.RemoveRange(matches);
    }

    public async Task<Match?> GetRandomMatchAsync(Guid userId)
    {
        var matches = await dbContext.Matches
            .Where(m => m.User.Id == userId && m.Swipe == null)
            .Include(m => m.User)
            .Include(m => m.MatchedUser)
            .Include(m => m.MatchedUser.Sports)
            .ToListAsync();

        var randomMatch = matches.MinBy(m => Guid.NewGuid());

        return randomMatch;
    }

    public Task RemoveRangeAsync(IEnumerable<Match> matches)
    {
        dbContext.Matches.RemoveRange(matches);

        return Task.CompletedTask;
    }
}