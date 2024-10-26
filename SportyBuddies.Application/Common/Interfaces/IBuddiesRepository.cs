using SportyBuddies.Domain.Buddies;

namespace SportyBuddies.Application.Common.Interfaces;

public interface IBuddiesRepository
{
    Task<IEnumerable<Buddy>> GetUserBuddiesAsync(Guid userId, bool includeUsers);
    Task AddBuddyAsync(Buddy buddy);
    Task<bool> AreUsersBuddiesAsync(Guid userId, Guid matchedUserId);
}