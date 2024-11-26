using SportyBuddies.Domain.Conversations;
using SportyBuddies.Domain.Matches;

namespace SportyBuddies.Domain.Services;

public interface IBuddyService
{
    Task AddBuddyAsync(Match match, Match oppositeMatch);
}