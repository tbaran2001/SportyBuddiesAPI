using Microsoft.EntityFrameworkCore;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Domain.Matches;
using SportyBuddies.Infrastructure.Common.Persistence;

namespace SportyBuddies.Infrastructure.Matches.Persistence;

public class MatchesesRepository : GenericRepository<Match>, IMatchesRepository
{
    public MatchesesRepository(SportyBuddiesDbContext dbContext) : base(dbContext)
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
}