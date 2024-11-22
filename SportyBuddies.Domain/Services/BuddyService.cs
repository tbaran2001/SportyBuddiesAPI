using SportyBuddies.Domain.Buddies;
using SportyBuddies.Domain.Matches;

namespace SportyBuddies.Domain.Services;

public class BuddyService(IBuddiesRepository buddiesRepository):IBuddyService
{
    public async Task AddBuddy(Match match, Match oppositeMatch)
    {
        if(match==oppositeMatch)
            throw new Exception("Match and opposite match cannot be the same");

        if(await buddiesRepository.AreUsersAlreadyBuddiesAsync(match.UserId,oppositeMatch.UserId))
            throw new Exception("Users are already buddies");

        if(match.Swipe != Swipe.Right || oppositeMatch.Swipe != Swipe.Right)
            throw new Exception("Both users must swipe right to become buddies");

        var now = DateTime.UtcNow;
        var buddy = Buddy.Create(match.UserId, oppositeMatch.UserId, now);
        var oppositeBuddy = Buddy.Create(oppositeMatch.UserId, match.UserId, now);

        await buddiesRepository.AddBuddyAsync(buddy);
        await buddiesRepository.AddBuddyAsync(oppositeBuddy);
    }
}