using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Profiles;

namespace SportyBuddies.Domain.Conversations;

public class Participant : Entity
{
    private Participant(
        Guid id,
        Guid conversationId,
        Guid profileId,
        DateTime createdOnUtc
    ) : base(id)
    {
        ConversationId = conversationId;
        ProfileId = profileId;
        CreatedOnUtc = createdOnUtc;
    }

    public Guid ConversationId { get; private set; }
    public Guid ProfileId { get; private set; }
    public DateTime CreatedOnUtc { get; private set; }

    public Conversation? Conversation { get; private set; }
    public Profile? Profile { get; private set; }

    public static Participant Create(Guid conversationId, Guid profileId)
    {
        return new Participant(
            id: Guid.NewGuid(),
            conversationId: conversationId,
            profileId: profileId,
            createdOnUtc: DateTime.UtcNow);
    }

    private Participant()
    {
    }
}