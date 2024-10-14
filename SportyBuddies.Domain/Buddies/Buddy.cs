using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Domain.Buddies;

public class Buddy : Entity
{
    public Buddy(User user, User matchedUser, DateTime matchDateTime, Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        User = user;
        MatchedUser = matchedUser;
        MatchDateTime = matchDateTime;
    }

    public Buddy()
    {
    }

    public User User { get; private set; }
    public User MatchedUser { get; private set; }
    public DateTime MatchDateTime { get; private set; }
}