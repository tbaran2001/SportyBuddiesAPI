using Microsoft.EntityFrameworkCore;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Domain.SportAggregate;
using SportyBuddies.Domain.SportAggregate.ValueObjects;
using SportyBuddies.Infrastructure.Common.Persistence;

namespace SportyBuddies.Infrastructure.Sports.Persistence;

public class SportsRepository(SportyBuddiesDbContext dbContext) : ISportsRepository
{
    public async Task<IEnumerable<Sport>> GetAllSportsAsync()
    {
        return await dbContext.Sports.ToListAsync();
    }

    public async Task<Sport?> GetSportByIdAsync(SportId sportId)
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