using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Conversations;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Domain.Buddies;

public class Buddy : Entity
{
    private Buddy(
        Guid id,
        Guid userId,
        Guid matchedUserId,
        DateTime createdOnUtc
    ) : base(id)
    {
        UserId = userId;
        MatchedUserId = matchedUserId;
        CreatedOnUtc = createdOnUtc;
    }

    public Guid OppositeBuddyId { get; private set; }
    public Guid UserId { get; private set; }
    public Guid MatchedUserId { get; private set; }
    public DateTime CreatedOnUtc { get; private set; }
    public Guid? ConversationId { get; private set; }

    public User? User { get; private set; }
    public User? MatchedUser { private set; get; }
    public Conversation? Conversation { get; private set; }

    public static (Buddy, Buddy) CreatePair(Guid userId, Guid matchedUserId, DateTime createdOnUtc)
    {
        var buddy1 = new Buddy(Guid.NewGuid(), userId, matchedUserId, createdOnUtc);
        var buddy2 = new Buddy(Guid.NewGuid(), matchedUserId, userId, createdOnUtc);

        buddy1.OppositeBuddyId = buddy2.Id;
        buddy2.OppositeBuddyId = buddy1.Id;

        return (buddy1, buddy2);
    }

    public void SetConversation(Conversation conversation)
    {
        Conversation = conversation;
        ConversationId = conversation.Id;
    }

    private Buddy()
    {
    }
}