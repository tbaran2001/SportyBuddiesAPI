using Microsoft.EntityFrameworkCore;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Domain.Sports;
using SportyBuddies.Domain.Users;
using SportyBuddies.Infrastructure.Common.Persistence;

namespace SportyBuddies.Infrastructure.UserSports.Persistence;

public class UserSportsRepository : IUserSportsRepository
{
    protected readonly SportyBuddiesDbContext _dbContext;

    public UserSportsRepository(SportyBuddiesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Sport>> GetUserSportsAsync(Guid userId)
    {
        return await _dbContext.Sports
            .Where(x => x.Users.Any(u => u.Id == userId))
            .ToListAsync();
    }

    public async Task AddSportToUserAsync(Guid userId, Guid sportId)
    {
        var user = await _dbContext.Users
            .Include(x => x.Sports)
            .FirstOrDefaultAsync(x => x.Id == userId);

        if (user == null)
            throw new NotFoundException(nameof(User), userId.ToString());

        var sport = await _dbContext.Sports
            .FirstOrDefaultAsync(x => x.Id == sportId);

        if (sport == null)
            throw new NotFoundException(nameof(Sport), sportId.ToString());

        if (user.Sports.Any(x => x.Id == sportId))
            throw new BadRequestException("User already has this sport");

        user.Sports.Add(sport);
    }

    public async Task RemoveSportFromUserAsync(Guid userId, Guid sportId)
    {
        var user = await _dbContext.Users
            .Include(x => x.Sports)
            .FirstOrDefaultAsync(x => x.Id == userId);

        if (user == null)
            throw new NotFoundException(nameof(User), userId.ToString());

        var sport = await _dbContext.Sports
            .FirstOrDefaultAsync(x => x.Id == sportId);

        if (sport == null)
            throw new NotFoundException(nameof(Sport), sportId.ToString());

        if (user.Sports.All(x => x.Id != sportId))
            throw new BadRequestException("User does not have this sport");

        user.Sports.Remove(sport);
    }

    public async Task<List<Guid>> GetUserSportsIdsAsync(Guid userId)
    {
        return await _dbContext.Sports
            .Where(x => x.Users.Any(u => u.Id == userId))
            .Select(x => x.Id)
            .ToListAsync();
    }
}