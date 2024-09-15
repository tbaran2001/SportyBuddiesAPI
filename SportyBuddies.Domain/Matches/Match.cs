using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Domain.Matches;

public class Match : Entity
{
    public Match(User user, User matchedUser, DateTime matchDateTime, Swipe? swipe, DateTime? swipeDateTime,
        Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        User = user;
        MatchedUser = matchedUser;
        MatchDateTime = matchDateTime;
        Swipe = swipe;
        SwipeDateTime = swipeDateTime;
    }

    public Match()
    {
    }

    public User User { get; private set; }
    public User MatchedUser { get; private set; }
    public DateTime MatchDateTime { get; private set; }
    public Swipe? Swipe { get; private set; }
    public DateTime? SwipeDateTime { get; private set; }
}