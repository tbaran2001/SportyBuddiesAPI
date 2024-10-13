using SportyBuddies.Domain.MatchAggregate;
using SportyBuddies.Domain.MatchAggregate.ValueObjects;
using SportyBuddies.Domain.UserAggregate.ValueObjects;

namespace SportyBuddies.Application.Common.Interfaces;

public interface IMatchesRepository
{
    Task<Match?> GetMatchByIdAsync(MatchId matchId);
    Task<IEnumerable<Match>> GetUserMatchesAsync(UserId userId);
    Task<IEnumerable<Match>> GetUserExistingMatchesAsync(UserId userId);
    Task AddMatchesAsync(IEnumerable<Match> matches);
    void RemoveMatches(IEnumerable<Match> matches);
    Task<Match?> GetRandomMatchAsync(UserId userId);
    Task RemoveRangeAsync(IEnumerable<Match> matches);
}