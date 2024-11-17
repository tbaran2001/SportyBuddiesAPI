using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Domain.Conversations;

public class Conversation : Entity
{
    private Conversation(
        Guid id,
        Guid creatorId,
        ICollection<Participant> participants,
        ICollection<Message> messages,
        DateTime createdOnUtc
    ) : base(id)
    {
        CreatorId = creatorId;
        Participants = participants;
        Messages = messages;
        CreatedOnUtc = createdOnUtc;
    }

    public Guid CreatorId { get; private set; }
    public User? Creator { get; private set; }
    public ICollection<Participant> Participants { get; private set; }
    public ICollection<Message> Messages { get; private set; }
    public DateTime CreatedOnUtc { get; private set; }

    public static Conversation Create(Guid creatorId, ICollection<Guid> participantIds)
    {
        return new Conversation(
            id: Guid.NewGuid(),
            creatorId: creatorId,
            participants: participantIds.Select(id => Participant.Create(Guid.NewGuid(), id)).ToList(),
            messages: new List<Message>(),
            createdOnUtc: DateTime.UtcNow
        );
    }

    private Conversation()
    {
    }
}