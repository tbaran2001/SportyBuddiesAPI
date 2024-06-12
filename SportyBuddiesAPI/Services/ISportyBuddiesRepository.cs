using SportyBuddiesAPI.Entities;

namespace SportyBuddiesAPI.Services;

public interface ISportyBuddiesRepository
{
    Task<IEnumerable<User>> GetUsersAsync();
    Task<User?> GetUser(int id, bool includeSports);
    Task<IEnumerable<Sport>> GetUserSportsAsync(int userId);
    Task<Sport?> GetUserSportAsync(int userId, int sportId);
    Task AddSportToUserAsync(int userId, int sportId);
    Task RemoveSportFromUserAsync(int userId, int sportId);
}