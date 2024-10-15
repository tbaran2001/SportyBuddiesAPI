using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Domain.Messages;

public class Message : Entity
{
    public Message(User sender, User recipient, string content, DateTime timeSent, Guid? id = null) : base(
        id ?? Guid.NewGuid())
    {
        Sender = sender;
        Recipient = recipient;
        Content = content;
        TimeSent = timeSent;
    }

    public Message()
    {
    }

    public User Sender { get; private set; }
    public User Recipient { get; private set; }
    public string Content { get; private set; }
    public DateTime TimeSent { get; private set; }
}