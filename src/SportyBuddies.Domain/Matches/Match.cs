using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Profiles;

namespace SportyBuddies.Domain.Matches;

public class Match : Entity
{
    private Match(
        Guid id,
        Guid profileId,
        Guid matchedProfileId,
        DateTime matchDateTime
    ) : base(id)
    {
        ProfileId = profileId;
        MatchedProfileId = matchedProfileId;
        MatchDateTime = matchDateTime;
    }

    public Guid OppositeMatchId { get; private set; }
    public Guid ProfileId { get; private set; }
    public Guid MatchedProfileId { get; private set; }
    public DateTime MatchDateTime { get; private set; }
    public Swipe? Swipe { get; private set; }
    public DateTime? SwipeDateTime { get; private set; }

    public Profile? Profile { get; private set; }
    public Profile? MatchedProfile { get; private set; }

    public static (Match, Match) CreatePair(Guid profileId, Guid matchedProfileId, DateTime matchDateTime)
    {
        var match1 = new Match(Guid.NewGuid(), profileId, matchedProfileId, matchDateTime);
        var match2 = new Match(Guid.NewGuid(), matchedProfileId, profileId, matchDateTime);

        match1.OppositeMatchId = match2.Id;
        match2.OppositeMatchId = match1.Id;

        return (match1, match2);
    }

    public void UpdateSwipe(Swipe swipe)
    {
        Swipe = swipe;
        SwipeDateTime = DateTime.UtcNow;
    }

    private Match()
    {
    }
}