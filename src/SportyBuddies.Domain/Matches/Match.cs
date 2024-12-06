using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Domain.Matches;

public class Match : Entity
{
    private Match(
        Guid id,
        Guid userId,
        Guid matchedUserId,
        DateTime matchDateTime
    ) : base(id)
    {
        UserId = userId;
        MatchedUserId = matchedUserId;
        MatchDateTime = matchDateTime;
    }

    public Guid OppositeMatchId { get; private set; }
    public Guid UserId { get; private set; }
    public Guid MatchedUserId { get; private set; }
    public DateTime MatchDateTime { get; private set; }
    public Swipe? Swipe { get; private set; }
    public DateTime? SwipeDateTime { get; private set; }

    public User? User { get; private set; }
    public User? MatchedUser { get; private set; }

    public static (Match, Match) CreatePair(Guid userId, Guid matchedUserId, DateTime matchDateTime)
    {
        var match1 = new Match(Guid.NewGuid(), userId, matchedUserId, matchDateTime);
        var match2 = new Match(Guid.NewGuid(), matchedUserId, userId, matchDateTime);

        match1.OppositeMatchId = match2.Id;
        match2.OppositeMatchId = match1.Id;

        return (match1, match2);
    }

    public void UpdateSwipe(Swipe swipe)
    {
        Swipe = swipe;
        SwipeDateTime = DateTime.UtcNow;
    }

    private Match()
    {
    }
}