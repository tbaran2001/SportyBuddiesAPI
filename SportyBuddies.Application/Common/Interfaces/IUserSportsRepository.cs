using SportyBuddies.Domain.Sports;

namespace SportyBuddies.Application.Common.Interfaces;

public interface IUserSportsRepository
{
    Task<IEnumerable<Sport>> GetUserSportsAsync(Guid userId);
    Task AddSportToUserAsync(Guid userId, Guid sportId);
    Task RemoveSportFromUserAsync(Guid userId, Guid sportId);
    Task<List<Guid>> GetUserSportsIdsAsync(Guid userId);
}