using Microsoft.EntityFrameworkCore;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Domain.Matches;
using SportyBuddies.Infrastructure.Common.Persistence;

namespace SportyBuddies.Infrastructure.Matches.Persistence;

public class MatchesRepository(SportyBuddiesDbContext dbContext) : IMatchesRepository
{
    public async Task<Match?> GetMatchByIdAsync(Guid matchId)
    {
        return await dbContext.Matches.FindAsync(matchId);
    }

    public async Task<Match?> GetMatchWithUsersByIdAsync(Guid matchId)
    {
        return await dbContext.Matches
            .Include(m => m.User)
            .Include(m => m.MatchedUser)
            .FirstOrDefaultAsync(m => m.Id == matchId);
    }

    public async Task<IEnumerable<Match>> GetUserMatchesAsync(Guid userId)
    {
        return await dbContext.Matches
            .Include(m => m.User)
            .Include(m => m.MatchedUser)
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

    public async Task<Match?> GetMatchByUserAndMatchedUserAsync(Guid matchedUserId, Guid userId)
    {
        return await dbContext.Matches
            .Include(m => m.User)
            .Include(m => m.MatchedUser)
            .FirstOrDefaultAsync(m =>
                (m.User.Id == userId && m.MatchedUser.Id == matchedUserId) ||
                (m.User.Id == matchedUserId && m.MatchedUser.Id == userId));
    }
}