using SportyBuddies.Domain.Common.Models;
using SportyBuddies.Domain.MatchAggregate.ValueObjects;
using SportyBuddies.Domain.UserAggregate.ValueObjects;

namespace SportyBuddies.Domain.MatchAggregate;

public class Match : AggregateRoot<MatchId>
{
    private Match(MatchId matchId, UserId userId, UserId matchedUserId, DateTime matchDateTime, Swipe? swipe,
        DateTime? swipeDateTime) : base(matchId)
    {
        UserId = userId;
        MatchedUserId = matchedUserId;
        MatchDateTime = matchDateTime;
        Swipe = swipe;
        SwipeDateTime = swipeDateTime;
    }

    private Match()
    {
    }

    public UserId UserId { get; private set; }
    public UserId MatchedUserId { get; private set; }
    public DateTime MatchDateTime { get; private set; }
    public Swipe? Swipe { get; private set; }
    public DateTime? SwipeDateTime { get; private set; }

    public static Match Create(UserId user, UserId matchedUser, DateTime matchDateTime, Swipe? swipe,
        DateTime? swipeDateTime)
    {
        var match = new Match(MatchId.CreateUnique(), user, matchedUser, matchDateTime, swipe, swipeDateTime);

        return match;
    }
}