using SportyBuddies.Domain.Matches;

namespace SportyBuddies.Application.Common.DTOs;

public record MatchResponse(
    Guid Id,
    UserResponse User,
    UserResponse MatchedUser,
    DateTime MatchDateTime,
    Swipe? Swipe,
    DateTime? SwipeDateTime);