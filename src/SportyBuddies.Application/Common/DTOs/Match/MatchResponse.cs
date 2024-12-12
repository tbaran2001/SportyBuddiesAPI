using SportyBuddies.Domain.Matches;

namespace SportyBuddies.Application.Common.DTOs.Match;

public record MatchResponse(
    Guid Id,
    Guid ProfileId,
    Guid MatchedProfileId,
    DateTime MatchDateTime,
    Swipe? Swipe,
    DateTime? SwipeDateTime);