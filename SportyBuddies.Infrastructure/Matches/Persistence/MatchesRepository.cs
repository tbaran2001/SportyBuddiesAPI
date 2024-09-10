using Microsoft.EntityFrameworkCore;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Domain.Matches;
using SportyBuddies.Infrastructure.Common.Persistence;

namespace SportyBuddies.Infrastructure.Matches.Persistence;

public class MatchesRepository : GenericRepository<Match>, IMatchesRepository
{
    public MatchesRepository(SportyBuddiesDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Match>> GetUserMatchesAsync(Guid userId)
    {
        return await _dbContext.Matches
            .Include(m => m.User)
            .Include(m => m.MatchedUser)
            .Where(m => m.User.Id == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Match>> GetUserExistingMatchesAsync(Guid userId)
    {
        return await _dbContext.Matches
            .Where(m => m.User.Id == userId || m.MatchedUser.Id == userId)
            .ToListAsync();
    }

    public async Task AddMatchesAsync(IEnumerable<Match> matches)
    {
        await _dbContext.Matches.AddRangeAsync(matches);
    }

    public void RemoveMatches(IEnumerable<Match> matches)
    {
        _dbContext.Matches.RemoveRange(matches);
    }

    public async Task<Match?> GetRandomMatchAsync(Guid userId)
    {
        var matches = await _dbContext.Matches
            .Where(m => m.User.Id == userId && m.Swipe == null)
            .Include(m => m.User)
            .Include(m => m.MatchedUser)
            .Include(m => m.MatchedUser.Sports)
            .ToListAsync();

        var randomMatch = matches.MinBy(m => Guid.NewGuid());

        return randomMatch;
    }
}