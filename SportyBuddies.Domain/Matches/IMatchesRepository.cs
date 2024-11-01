namespace SportyBuddies.Domain.Matches;

public interface IMatchesRepository
{
    Task<Match?> GetMatchByIdAsync(Guid matchId);
    Task<Match?> GetMatchWithUsersAsync(Guid matchId);
    Task<IEnumerable<Match>> GetUserMatchesAsync(Guid userId);
    Task<IEnumerable<Match>> GetUserExistingMatchesAsync(Guid userId);
    Task AddMatchesAsync(IEnumerable<Match> matches);
    void RemoveMatches(IEnumerable<Match> matches);
    Task<Match?> GetRandomMatchAsync(Guid userId);
    Task RemoveRangeAsync(IEnumerable<Match> matches);
    Task<Match?> GetMatchByUserAndMatchedUserAsync(Guid matchedUserId, Guid userId);
}