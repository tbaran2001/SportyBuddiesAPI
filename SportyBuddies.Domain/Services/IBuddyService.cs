using SportyBuddies.Domain.Matches;

namespace SportyBuddies.Domain.Services;

public interface IBuddyService
{
    Task AddBuddy(Match match, Match oppositeMatch);
}