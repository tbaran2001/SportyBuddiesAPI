using SportyBuddies.Domain.Matches;

namespace SportyBuddies.Application.Common.Interfaces;

public interface IMatchesRepository
{
    Task<Match?> GetMatchByIdAsync(Guid matchId);
    Task<Match?> GetMatchWithUsersByIdAsync(Guid matchId);
    Task<IEnumerable<Match>> GetUserMatchesAsync(Guid userId, bool includeUsers);
    Task<IEnumerable<Match>> GetUserExistingMatchesAsync(Guid userId);
    Task AddMatchesAsync(IEnumerable<Match> matches);
    void RemoveMatches(IEnumerable<Match> matches);
    Task<Match?> GetRandomMatchAsync(Guid userId);
    Task RemoveRangeAsync(IEnumerable<Match> matches);
    Task<Match?> GetMatchByUserAndMatchedUserAsync(Guid matchedUserId, Guid userId);
}