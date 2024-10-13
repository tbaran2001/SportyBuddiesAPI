using SportyBuddies.Domain.UserAggregate.ValueObjects;

namespace SportyBuddies.Application.Common.Services;

public interface IMatchingService
{
    Task FindMatchesAsync(UserId userId);
}