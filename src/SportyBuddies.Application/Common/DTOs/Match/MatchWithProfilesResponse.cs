using SportyBuddies.Application.Common.DTOs.Profile;
using SportyBuddies.Domain.Matches;

namespace SportyBuddies.Application.Common.DTOs.Match;

public record MatchWithProfilesResponse(
    Guid Id,
    ProfileResponse Profile,
    ProfileResponse MatchedProfile,
    DateTime MatchDateTime,
    Swipe? Swipe,
    DateTime? SwipeDateTime);