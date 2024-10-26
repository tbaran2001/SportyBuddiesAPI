using Microsoft.EntityFrameworkCore;
using SportyBuddies.Domain.Sports;
using SportyBuddies.Infrastructure.Common.Persistence;

namespace SportyBuddies.Infrastructure.Sports.Persistence;

public class SportsRepository(SportyBuddiesDbContext dbContext) : ISportsRepository
{
    public async Task<IEnumerable<Sport>> GetAllSportsAsync()
    {
        return await dbContext.Sports.ToListAsync();
    }

    public async Task<Sport?> GetSportByIdAsync(Guid sportId)
    {
        return await dbContext.Sports.FindAsync(sportId);
    }

    public async Task AddSportAsync(Sport sport)
    {
        await dbContext.Sports.AddAsync(sport);
    }

    public void RemoveSport(Sport sport)
    {
        dbContext.Sports.Remove(sport);
    }
}