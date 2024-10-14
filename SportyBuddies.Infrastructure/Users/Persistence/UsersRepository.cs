using Microsoft.EntityFrameworkCore;
using SportyBuddies.Application.Common.DTOs;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Domain.UserAggregate;
using SportyBuddies.Domain.UserAggregate.ValueObjects;
using SportyBuddies.Infrastructure.Common.Persistence;

namespace SportyBuddies.Infrastructure.Users.Persistence;

public class UsersRepository(SportyBuddiesDbContext dbContext) : IUsersRepository
{
    public async Task<User?> GetUserByIdAsync(UserId userId)
    {
        return await dbContext.Users.FindAsync(userId);
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await dbContext.Users.ToListAsync();
    }

    public async Task AddUserAsync(User user)
    {
        await dbContext.Users.AddAsync(user);
    }

    public void RemoveUser(User user)
    {
        dbContext.Users.Remove(user);
    }

    public async Task<User?> GetUserByIdWithSportsAsync(UserId userId)
    {
        return await dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task<UserWithSportsResponse?> GetUserByIdWithSportsResponseAsync(UserId userId)
    {
        // Fetch the user and sport IDs first from the database
        var user = await dbContext.Users
            .Where(u => u.Id == userId)
            .Select(u => new
            {
                u.Id,
                u.Name,
                u.Description,
                u.LastActive,
                SportIds = u.SportIds.Select(s => s.Value).ToList() // Extract primitive SportId values
            })
            .FirstOrDefaultAsync();

        if (user == null) return null;

        // Switch to client-side evaluation for the Sport filtering
        var sports = dbContext.Sports
            .AsEnumerable() // Forces EF to bring all data into memory for further filtering
            .Where(s => user.SportIds.Contains(s.Id.Value)) // Perform the filtering in-memory
            .ToList();

        // Return the result with the user and their associated sports
        return new UserWithSportsResponse(
            user.Id.Value,
            user.Name,
            user.Description,
            user.LastActive,
            sports.Select(s => new SportResponse(s.Id.Value, s.Name, s.Description)).ToList() // Convert to DTOs
        );
    }

    public async Task<IEnumerable<User>> GetAllUsersWithSportsAsync()
    {
        return await dbContext.Users
            .ToListAsync();
    }
}