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
}