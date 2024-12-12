using SportyBuddies.Application.Common.DTOs.Profile;

namespace SportyBuddies.Application.Common.DTOs.Buddy;

public record BuddyWithProfilesResponse(Guid Id, ProfileResponse Profile, ProfileResponse MatchedProfile, DateTime MatchDateTime);