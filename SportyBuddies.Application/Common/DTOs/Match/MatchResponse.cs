using SportyBuddies.Domain.Matches;

namespace SportyBuddies.Application.Common.DTOs.Match;

public record MatchResponse(
    Guid Id,
    Guid UserId,
    Guid MatchedUserId,
    DateTime MatchDateTime,
    Swipe? Swipe,
    DateTime? SwipeDateTime);