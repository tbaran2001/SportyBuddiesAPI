using SportyBuddies.Domain.Users;

namespace SportyBuddies.Domain.Matches;

public class Match
{
    public Match(User user, User matchedUser, DateTime matchDateTime, Swipe? swipe, DateTime? swipeDateTime,
        Guid? id = null)
    {
        User = user;
        MatchedUser = matchedUser;
        MatchDateTime = matchDateTime;
        Swipe = swipe;
        SwipeDateTime = swipeDateTime;
        Id = id ?? Guid.NewGuid();
    }

    public Match()
    {
    }

    public Guid Id { get; private set; }
    public User User { get; private set; }
    public User MatchedUser { get; private set; }
    public DateTime MatchDateTime { get; private set; }
    public Swipe? Swipe { get; private set; }
    public DateTime? SwipeDateTime { get; private set; }
}