using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Domain.Conversations;

public class Participant : Entity
{
    private Participant(
        Guid id,
        Guid conversationId,
        Guid userId,
        DateTime createdAt
    ) : base(id)
    {
        ConversationId = conversationId;
        UserId = userId;
        CreatedAt = createdAt;
    }

    public Guid ConversationId { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Conversation? Conversation { get; private set; }
    public User? User { get; private set; }

    public static Participant Create(Guid conversationId, Guid userId)
    {
        return new Participant(
            id: Guid.NewGuid(),
            conversationId: conversationId,
            userId: userId,
            createdAt: DateTime.UtcNow);
    }

    private Participant()
    {
    }
}