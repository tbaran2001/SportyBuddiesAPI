using SportyBuddies.Domain.Profiles;

namespace SportyBuddies.Api.Contracts.Profiles;

public record UpdateProfilePreferencesRequest(int MinAge, int MaxAge, int MaxDistance, Gender Gender);