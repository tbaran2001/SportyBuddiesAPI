using Microsoft.EntityFrameworkCore;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Domain.Sports;
using SportyBuddies.Infrastructure.Common.Persistence;

namespace SportyBuddies.Infrastructure.Sports.Persistence;

public class SportsRepository : GenericRepository<Sport>,ISportsRepository
{
    public SportsRepository(SportyBuddiesDbContext dbContext):base(dbContext)
    {
    }

    public async Task<bool> SportExistsAsync(Guid id)
    {
        return await _dbContext.Sports.AnyAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Sport>> GetUserSportsAsync(Guid userId)
    {
        return await _dbContext.Sports
            .Where(x => x.Users.Any(u => u.Id == userId))
            .ToListAsync();
    }
}