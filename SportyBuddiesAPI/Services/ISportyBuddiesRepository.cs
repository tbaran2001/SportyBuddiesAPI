using SportyBuddiesAPI.Entities;

namespace SportyBuddiesAPI.Services;

public interface ISportyBuddiesRepository
{
    Task<IEnumerable<User>> GetUsersAsync();
    Task<User?> GetUserAsync(int userId, bool includeSports);
    Task<bool> UserExistsAsync(int userId);
    Task<IEnumerable<Sport>> GetUserSportsAsync(int userId);
    Task<Sport?> GetUserSportAsync(int userId, int sportId);
    Task<IEnumerable<Sport>> GetSportsAsync();
    Task<Sport?> GetSportAsync(int sportId);
    Task<bool> SportExistsAsync(int sportId);
    Task AddSportAsync(Sport sport);
    void DeleteSport(Sport sport);
    Task<bool> HasSportAsync(int userId, int sportId);
    Task AddSportToUserAsync(int userId, int sportId);
    Task RemoveSportFromUserAsync(int userId, int sportId);
    Task<IEnumerable<Match>> GetMatchesAsync();
    Task<Match?> GetMatchAsync(int matchId);
    Task<Match?> GetMatchAsync(int userId, int matchedUserId);
    Task<IEnumerable<Match>> GetUserMatchesAsync(int userId);
    Task<bool> MatchExistsAsync(int userId, int matchedUserId);
    Task UpdateUserMatchesAsync(int userId);
    Task<bool> SaveChangesAsync();
}