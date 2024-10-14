namespace SportyBuddies.Application.Common.Services;

public interface IMatchingService
{
    Task FindMatchesAsync(Guid userId);
}