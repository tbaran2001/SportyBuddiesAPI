using SportyBuddies.Application.Common.DTOs.User;
using SportyBuddies.Domain.Matches;

namespace SportyBuddies.Application.Common.DTOs.Match;

public record RandomMatchResponse(
    Guid Id,
    Guid UserId,
    UserWithSportsResponse MatchedUser,
    DateTime MatchDateTime,
    Swipe? Swipe,
    DateTime? SwipeDateTime);