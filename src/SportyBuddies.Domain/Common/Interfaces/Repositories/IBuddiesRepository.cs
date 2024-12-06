using SportyBuddies.Domain.Buddies;

namespace SportyBuddies.Domain.Common.Interfaces.Repositories;

public interface IBuddiesRepository
{
    Task<IEnumerable<Buddy>> GetUserBuddiesAsync(Guid userId);
    Task AddBuddyAsync(Buddy buddy);
    Task<bool> AreUsersAlreadyBuddiesAsync(Guid userId, Guid matchedUserId);
    Task RemoveUserBuddiesAsync(Guid userId);
    Task<IEnumerable<Buddy>> GetRelatedBuddies(Guid userId, Guid matchedUserId);
}