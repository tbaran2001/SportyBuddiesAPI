using SportyBuddiesAPI.DbContexts;
using SportyBuddiesAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace SportyBuddiesAPI.Services;

public class SportyBuddiesRepository : ISportyBuddiesRepository
{
    private readonly SportyBuddiesContext _context;

    public SportyBuddiesRepository(SportyBuddiesContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> GetUser(int id, bool includeSports = false)
    {
        if(includeSports)
        {
            return await _context.Users
                .Include(u => u.UserSports)
                .ThenInclude(us => us.Sport)
                .FirstOrDefaultAsync(u => u.Id == id);
        }
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<IEnumerable<Sport>> GetUserSportsAsync(int userId)
    {
        return await _context.UserSports
            .Where(us => us.UserId == userId)
            .Select(us => us.Sport)
            .ToListAsync();
    }

    public async Task<Sport?> GetUserSportAsync(int userId, int sportId)
    {
        return await _context.UserSports
            .Where(us => us.UserId == userId && us.SportId == sportId)
            .Select(us => us.Sport)
            .FirstOrDefaultAsync();
    }

    public async Task AddSportToUserAsync(int userId, int sportId)
    {
        throw new NotImplementedException();
    }

    public async Task RemoveSportFromUserAsync(int userId, int sportId)
    {
        throw new NotImplementedException();
    }
}