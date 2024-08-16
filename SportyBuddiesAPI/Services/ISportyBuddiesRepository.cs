using SportyBuddiesAPI.Entities;

namespace SportyBuddiesAPI.Services;

public interface ISportyBuddiesRepository
{
    Task<IEnumerable<User>> GetUsersAsync();
    Task<(IEnumerable<User>,PaginationMetaData)> GetUsersAsync(string? name, string? searchQuery, int pageNumber, int pageSize, bool includeSports);
    Task<User?> GetUserAsync(string userId, bool includeSports);
    Task AddUserAsync(User user);
    Task<bool> UserExistsAsync(string userId);
    Task<IEnumerable<Sport>> GetUserSportsAsync(string userId);
    Task<Sport?> GetUserSportAsync(string userId, int sportId);
    Task<IEnumerable<Sport>> GetSportsAsync();
    Task<Sport?> GetSportAsync(int sportId);
    Task<bool> SportExistsAsync(int sportId);
    Task AddSportAsync(Sport sport);
    void DeleteSport(Sport sport);
    Task<bool> HasSportAsync(string userId, int sportId);
    Task AddSportToUserAsync(string userId, int sportId);
    Task RemoveSportFromUserAsync(string userId, int sportId);
    Task<IEnumerable<Match>> GetMatchesAsync();
    Task<Match?> GetMatchAsync(int matchId);
    Task<Match?> GetMatchAsync(string userId, string matchedUserId);
    Task<Match?> GetRandomMatchAsync(string userId);
    Task<IEnumerable<Match>> GetUserMatchesAsync(string userId);
    Task<bool> MatchExistsAsync(string userId, string matchedUserId);
    Task UpdateUserMatchesAsync(string userId);
    Task<bool> SaveChangesAsync();
}