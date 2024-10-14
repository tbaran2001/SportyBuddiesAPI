using Microsoft.EntityFrameworkCore;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Domain.MatchAggregate;
using SportyBuddies.Domain.MatchAggregate.ValueObjects;
using SportyBuddies.Domain.UserAggregate.ValueObjects;
using SportyBuddies.Infrastructure.Common.Persistence;

namespace SportyBuddies.Infrastructure.Matches.Persistence;

public class MatchesRepository(SportyBuddiesDbContext dbContext) : IMatchesRepository
{
    public async Task<Match?> GetMatchByIdAsync(MatchId matchId)
    {
        return await dbContext.Matches.FindAsync(matchId);
    }

    public async Task<IEnumerable<Match>> GetUserMatchesAsync(UserId userId)
    {
        return await dbContext.Matches
            .Where(m => m.UserId == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Match>> GetUserExistingMatchesAsync(UserId userId)
    {
        return await dbContext.Matches
            .Where(m => m.UserId == userId || m.MatchedUserId == userId)
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

    public async Task<Match?> GetRandomMatchAsync(UserId userId)
    {
        var matches = await dbContext.Matches
            .Where(m => m.UserId == userId && m.Swipe == null)
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