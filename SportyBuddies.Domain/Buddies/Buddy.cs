using SportyBuddies.Domain.Buddies.Events;
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
        DateTime matchDateTime
    ) : base(id)
    {
        UserId = userId;
        MatchedUserId = matchedUserId;
        MatchDateTime = matchDateTime;
    }

    public User? User { get; private set; }
    public Guid UserId { get; private set; }
    public User? MatchedUser { get; private set; }
    public Guid MatchedUserId { get; private set; }
    public DateTime MatchDateTime { get; private set; }

    public Guid? ConversationId { get; private set; }
    public Conversation? Conversation { get; private set; }

    public static Buddy Create(
        Guid userId,
        Guid matchedUserId,
        DateTime matchDateTime)
    {
        var buddy = new Buddy(
            Guid.NewGuid(),
            userId,
            matchedUserId,
            matchDateTime);

        buddy.AddDomainEvent(new BuddyCreatedEvent(userId, matchedUserId, matchDateTime));

        return buddy;
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