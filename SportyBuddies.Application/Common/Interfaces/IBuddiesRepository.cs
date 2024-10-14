using SportyBuddies.Domain.Buddies;

namespace SportyBuddies.Application.Common.Interfaces;

public interface IBuddiesRepository
{
    Task<IEnumerable<Buddy>> GetUserBuddiesAsync(Guid userId);
    Task AddBuddyAsync(Buddy buddy);
}