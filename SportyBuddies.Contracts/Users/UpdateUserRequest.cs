namespace SportyBuddies.Contracts.Users;

public record UpdateUserRequest(string Name, string Description, Gender Gender, DateOnly DateOfBirth);