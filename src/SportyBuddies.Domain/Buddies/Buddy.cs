using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Conversations;
using SportyBuddies.Domain.Profiles;

namespace SportyBuddies.Domain.Buddies;

public class Buddy : Entity
{
    private Buddy(
        Guid id,
        Guid profileId,
        Guid matchedProfileId,
        DateTime createdOnUtc
    ) : base(id)
    {
        ProfileId = profileId;
        MatchedProfileId = matchedProfileId;
        CreatedOnUtc = createdOnUtc;
    }

    public Guid OppositeBuddyId { get; private set; }
    public Guid ProfileId { get; private set; }
    public Guid MatchedProfileId { get; private set; }
    public DateTime CreatedOnUtc { get; private set; }
    public Guid? ConversationId { get; private set; }

    public Profile? Profile { get; private set; }
    public Profile? MatchedProfile { private set; get; }
    public Conversation? Conversation { get; private set; }

    public static (Buddy, Buddy) CreatePair(Guid profileId, Guid matchedProfileId, DateTime createdOnUtc)
    {
        var buddy1 = new Buddy(Guid.NewGuid(), profileId, matchedProfileId, createdOnUtc);
        var buddy2 = new Buddy(Guid.NewGuid(), matchedProfileId, profileId, createdOnUtc);

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