namespace SportyBuddies.Application.Common.Services;

public interface IMatchingService
{
    Task FindMatchesAsync(Guid userId);
    Task CreateBuddyRelationshipAsync(Guid matchId);
    Task<bool> AreUsersBuddiesAsync(Guid userId, Guid matchedUserId);
}