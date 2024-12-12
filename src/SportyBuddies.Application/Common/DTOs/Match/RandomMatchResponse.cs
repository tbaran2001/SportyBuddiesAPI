using SportyBuddies.Application.Common.DTOs.Profile;
using SportyBuddies.Domain.Matches;

namespace SportyBuddies.Application.Common.DTOs.Match;

public record RandomMatchResponse(
    Guid Id,
    Guid ProfileId,
    ProfileWithSportsResponse MatchedProfile,
    DateTime MatchDateTime,
    Swipe? Swipe,
    DateTime? SwipeDateTime);