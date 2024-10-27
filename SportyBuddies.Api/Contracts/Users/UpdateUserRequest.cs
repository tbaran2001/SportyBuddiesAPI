using SportyBuddies.Domain.Users;

namespace SportyBuddies.Api.Contracts.Users;

public record UpdateUserRequest(string Name, string Description, Gender Gender, DateOnly DateOfBirth);