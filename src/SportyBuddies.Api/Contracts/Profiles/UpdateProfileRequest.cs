using SportyBuddies.Domain.Profiles;

namespace SportyBuddies.Api.Contracts.Profiles;

public record UpdateProfileRequest(string Name, string Description, Gender Gender, DateOnly DateOfBirth);