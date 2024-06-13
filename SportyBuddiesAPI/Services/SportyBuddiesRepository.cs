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

    public async Task<User?> GetUserAsync(int userId, bool includeSports = false)
    {
        if (includeSports)
        {
            return await _context.Users
                .Include(u => u.Sports)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        return await _context.Users
            .FirstOrDefaultAsync(u => u.Id == userId);
    }

    public Task<bool> UserExistsAsync(int userId)
    {
        return _context.Users.AnyAsync(u => u.Id == userId);
    }

    public async Task<IEnumerable<Sport>> GetUserSportsAsync(int userId)
    {
        return await _context.Sports
            .Where(s => s.Users.Any(u => u.Id == userId))
            .ToListAsync();
    }

    public async Task<Sport?> GetUserSportAsync(int userId, int sportId)
    {
        return await _context.Sports
            .Where(s => s.Users.Any(u => u.Id == userId) && s.Id == sportId)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Sport>> GetSportsAsync()
    {
        return await _context.Sports.ToListAsync();
    }

    public async Task<Sport?> GetSportAsync(int sportId)
    {
        return await _context.Sports
            .FirstOrDefaultAsync(s => s.Id == sportId);
    }

    public Task<bool> SportExistsAsync(int sportId)
    {
        return _context.Sports.AnyAsync(s => s.Id == sportId);
    }

    public async Task AddSportAsync(Sport sport)
    {
        await _context.Sports.AddAsync(sport);
    }

    public void DeleteSport(Sport sport)
    {
        _context.Sports.Remove(sport);
    }

    public Task<bool> HasSportAsync(int userId, int sportId)
    {
        return _context.Users
            .Where(u => u.Id == userId)
            .SelectMany(u => u.Sports)
            .AnyAsync(s => s.Id == sportId);
    }

    public async Task AddSportToUserAsync(int userId, int sportId)
    {
        var user = await _context.Users
            .Include(u => u.Sports)
            .FirstOrDefaultAsync(u => u.Id == userId);

        var sport = await _context.Sports
            .FirstOrDefaultAsync(s => s.Id == sportId);

        user.Sports.Add(sport);
    }

    public async Task RemoveSportFromUserAsync(int userId, int sportId)
    {
        var user = await _context.Users
            .Include(u => u.Sports)
            .FirstOrDefaultAsync(u => u.Id == userId);

        var sport = await _context.Sports
            .FirstOrDefaultAsync(s => s.Id == sportId);

        user.Sports.Remove(sport);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return (await _context.SaveChangesAsync() >= 0);
    }
}