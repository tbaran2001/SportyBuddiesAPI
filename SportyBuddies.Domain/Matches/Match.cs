using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Domain.Matches;

public class Match : Entity
{
    private Match(
        Guid id,
        User user,
        User matchedUser,
        DateTime matchDateTime
    ) : base(id)
    {
        User = user;
        MatchedUser = matchedUser;
        MatchDateTime = matchDateTime;
        UserId = user.Id;
        MatchedUserId = matchedUser.Id;
    }

    public Match()
    {
    }

    public User User { get; private set; }
    public Guid UserId { get; private set; }
    public User MatchedUser { get; private set; }
    public Guid MatchedUserId { get; private set; }
    public DateTime MatchDateTime { get; private set; }
    public Swipe? Swipe { get; private set; }
    public DateTime? SwipeDateTime { get; private set; }

    public static Match Create(User user, User matchedUser, DateTime matchDateTime)
    {
        return new Match(Guid.NewGuid(), user, matchedUser, matchDateTime);
    }
    
    public void UpdateSwipe(Swipe swipe)
    {
        Swipe = swipe;
        SwipeDateTime = DateTime.UtcNow;
    }
}