using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Domain.Buddies;

public class Buddy : Entity
{
    private Buddy(
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
    
    public static Buddy Create(
        User user, 
        User matchedUser,
        DateTime matchDateTime)
    {
        return new Buddy(
            Guid.NewGuid(), 
            user, 
            matchedUser, 
            matchDateTime);
    }

    public User User { get; private set; }
    public Guid UserId { get; private set; }
    public User MatchedUser { get; private set; }
    public Guid MatchedUserId { get; private set; }
    public DateTime MatchDateTime { get; private set; }

    private Buddy()
    {
    }
}