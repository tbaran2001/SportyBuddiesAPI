namespace SportyBuddies.Domain.Buddies;

public interface IBuddiesRepository
{
    Task<IEnumerable<Buddy>> GetUserBuddiesAsync(Guid userId);
    Task AddBuddyAsync(Buddy buddy);
    Task<bool> AreUsersBuddiesAsync(Guid userId, Guid matchedUserId);
    Task RemoveUserBuddiesAsync(Guid userId);
}