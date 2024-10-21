using SportyBuddies.Domain.Matches;

namespace SportyBuddies.Application.Common.DTOs;

public record RandomMatchResponse(
    Guid Id,
    Guid UserId,
    UserWithSportsResponse MatchedUser,
    DateTime MatchDateTime,
    Swipe? Swipe,
    DateTime? SwipeDateTime);