using SportyBuddies.Domain.Matches;

namespace SportyBuddies.Application.Common.DTOs;

public record MatchResponse(
    Guid Id,
    UserWithSportsResponse UserWithSports,
    UserWithSportsResponse MatchedUserWithSports,
    DateTime MatchDateTime,
    Swipe? Swipe,
    DateTime? SwipeDateTime);