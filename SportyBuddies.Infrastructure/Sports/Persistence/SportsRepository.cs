using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Domain.Sports;
using SportyBuddies.Infrastructure.Common.Persistence;

namespace SportyBuddies.Infrastructure.Sports.Persistence;

public class SportsRepository : ISportsRepository
{
    private readonly SportyBuddiesDbContext _dbContext;

    public SportsRepository(SportyBuddiesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddSportAsync(Sport sport)
    {
        await _dbContext.Sports.AddAsync(sport);
    }

    public async Task<Sport?> GetByIdAsync(Guid sportId)
    {
        return await _dbContext.Sports.FindAsync(sportId);
    }

    public Task RemoveSportAsync(Sport sport)
    {
        _dbContext.Sports.Remove(sport);

        return Task.CompletedTask;
    }
}