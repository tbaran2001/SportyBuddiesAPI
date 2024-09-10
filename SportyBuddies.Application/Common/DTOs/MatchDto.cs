using SportyBuddies.Domain.Matches;

namespace SportyBuddies.Application.Common.DTOs;

public record MatchDto(
    Guid Id,
    UserDto User,
    UserDto MatchedUser,
    DateTime MatchDateTime,
    Swipe? Swipe,
    DateTime? SwipeDateTime);