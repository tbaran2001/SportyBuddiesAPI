using SportyBuddies.Domain.Matches;

namespace SportyBuddies.Application.Common.Interfaces;

public interface IMatchesRepository
{
    Task<Match?> GetMatchByIdAsync(Guid matchId);
    Task<IEnumerable<Match>> GetUserMatchesAsync(Guid userId);
    Task<IEnumerable<Match>> GetUserExistingMatchesAsync(Guid userId);
    Task AddMatchesAsync(IEnumerable<Match> matches);
    void RemoveMatches(IEnumerable<Match> matches);

    Task<Match?> GetRandomMatchAsync(Guid userId);

    //get user matches no matter if the user is the one who swiped or the one who got swiped
    Task RemoveRangeAsync(IEnumerable<Match> matches);
}