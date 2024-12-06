using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Domain.Conversations;

public class Message : Entity
{
    private Message(
        Guid id,
        Guid conversationId,
        Guid senderId,
        string content,
        DateTime createdOnUtc
    ) : base(id)
    {
        ConversationId = conversationId;
        SenderId = senderId;
        Content = content;
        CreatedOnUtc = createdOnUtc;
    }

    public Guid ConversationId { get; private set; }
    public Guid SenderId { get; private set; }
    public string Content { get; private set; }
    public DateTime CreatedOnUtc { get; private set; }

    public Conversation? Conversation { get; private set; }
    public User? Sender { get; private set; }

    public static Message Create(Guid conversationId, Guid senderId, string content)
    {
        return new Message(
            id: Guid.NewGuid(),
            conversationId: conversationId,
            senderId: senderId,
            content: content,
            DateTime.UtcNow);
    }

    private Message()
    {
    }
}