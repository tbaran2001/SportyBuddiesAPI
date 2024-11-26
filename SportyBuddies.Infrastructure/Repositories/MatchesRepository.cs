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
            .Where(m => m.UserId == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Match>> GetUserExistingMatchesAsync(Guid userId)
    {
        return await dbContext.Matches
            .Where(m => m.UserId == userId || m.MatchedUserId == userId)
            .ToListAsync();
    }

    public async Task AddMatchesAsync(IEnumerable<Match> matches)
    {
        await dbContext.Matches.AddRangeAsync(matches);
    }

    public async Task<Match?> GetRandomMatchAsync(Guid userId)
    {
        var matches = await dbContext.Matches
            .Where(m => m.UserId == userId && m.Swipe == null)
            .Include(m => m.User)
            .Include(m => m.MatchedUser)
            .ThenInclude(u => u.Sports)
            .ToListAsync();

        var randomMatch = matches.MinBy(m => Guid.NewGuid());

        return randomMatch;
    }

    public Task RemoveRangeAsync(IEnumerable<Match> matches)
    {
        dbContext.Matches.RemoveRange(matches);

        return Task.CompletedTask;
    }

    public async Task RemoveInvalidMatchesForUserAsync(Guid userId)
    {
        await dbContext.Matches
            .Where(m => (m.UserId == userId || m.MatchedUserId == userId) &&
                        !dbContext.Users
                            .Where(u => u.Id == m.UserId)
                            .SelectMany(u => u.Sports)
                            .Select(s => s.Id)
                            .Intersect(
                                dbContext.Users
                                    .Where(u => u.Id == m.MatchedUserId)
                                    .SelectMany(u => u.Sports)
                                    .Select(s => s.Id)
                            ).Any()
            )
            .ExecuteDeleteAsync();
    }
}