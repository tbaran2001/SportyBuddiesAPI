using SportyBuddies.Domain.Matches;

namespace SportyBuddies.Domain.Common.Interfaces.Services;

public interface IBuddyService
{
    Task AddBuddyAsync(Match match, Match oppositeMatch);
}