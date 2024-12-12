using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Profiles;

namespace SportyBuddies.Domain.Conversations;

public class Conversation : Entity
{
    private Conversation(
        Guid id,
        Guid creatorId,
        ICollection<Participant> participants,
        DateTime createdOnUtc
    ) : base(id)
    {
        CreatorId = creatorId;
        Participants = participants;
        CreatedOnUtc = createdOnUtc;
    }

    public Guid CreatorId { get; private set; }
    public DateTime CreatedOnUtc { get; private set; }
    public ICollection<Participant> Participants { get; private set; }
    public ICollection<Message> Messages { get; private set; } = new List<Message>();

    public Profile? Creator { get; private set; }


    public static Conversation CreateOneToOne(Guid creatorId, Guid participantId)
    {
        if (creatorId == participantId)
        {
            throw new InvalidOperationException("A creator cannot have a one-to-one conversation with themselves.");
        }

        return new Conversation(
            id: Guid.NewGuid(),
            creatorId: creatorId,
            participants: new List<Participant>
            {
                Participant.Create(Guid.NewGuid(), creatorId),
                Participant.Create(Guid.NewGuid(), participantId)
            },
            createdOnUtc: DateTime.UtcNow
        );
    }

    private Conversation()
    {
    }
}