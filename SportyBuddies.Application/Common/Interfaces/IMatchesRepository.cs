using SportyBuddies.Domain.Matches;

namespace SportyBuddies.Application.Common.Interfaces;

public interface IMatchesRepository : IGenericRepository<Match>
{
    Task<IEnumerable<Match>> GetUserMatchesAsync(Guid userId);
    Task<IEnumerable<Match>> GetUserExistingMatchesAsync(Guid userId);
    Task AddMatchesAsync(IEnumerable<Match> matches);
    void RemoveMatches(IEnumerable<Match> matches);
}