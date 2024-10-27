using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Domain.Messages;

public class Message : Entity
{
    private Message(
        Guid id,
        Guid senderId,
        Guid recipientId,
        string content,
        DateTime timeSent
    ) : base(id)
    {
        SenderId = senderId;
        RecipientId = recipientId;
        Content = content;
        TimeSent = timeSent;
    }
    
    public static Message Create(
        Guid senderId, 
        Guid recipientId, 
        string content,
        DateTime utcNow)
    {
        return new Message(
            Guid.NewGuid(), 
            senderId, 
            recipientId, 
            content, 
            utcNow);
    }

    public Message()
    {
    }

    public Guid SenderId { get; private set; }
    public Guid RecipientId { get; private set; }
    public string Content { get; private set; }
    public DateTime TimeSent { get; private set; }
}