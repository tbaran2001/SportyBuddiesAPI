using SportyBuddies.Application.Common.DTOs.User;
using SportyBuddies.Domain.Matches;

namespace SportyBuddies.Application.Common.DTOs.Match;

public record MatchWithUsersResponse(
    Guid Id,
    UserResponse User,
    UserResponse MatchedUser,
    DateTime MatchDateTime,
    Swipe? Swipe,
    DateTime? SwipeDateTime);