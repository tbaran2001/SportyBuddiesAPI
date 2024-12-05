using Microsoft.EntityFrameworkCore;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
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
        var count = await dbContext.Matches.CountAsync(m => m.UserId == userId && m.Swipe == null);
        if (count == 0)
            return null;

        var randomIndex = new Random().Next(count);

        return await dbContext.Matches
            .Where(m => m.UserId == userId && m.Swipe == null)
            .Include(m => m.User)
            .Include(m => m.MatchedUser)
            .ThenInclude(u => u.Sports)
            .Skip(randomIndex)
            .Take(1)
            .FirstOrDefaultAsync();
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