using SportyBuddies.Domain.Matches;

namespace SportyBuddies.Application.Common.Interfaces;

public interface IMatchesRepository : IGenericRepository<Match>
{
    Task<IEnumerable<Match>> GetUserMatchesAsync(Guid userId);
}