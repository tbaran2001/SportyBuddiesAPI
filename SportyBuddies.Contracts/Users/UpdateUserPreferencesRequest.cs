namespace SportyBuddies.Contracts.Users;

public record UpdateUserPreferencesRequest(int MinAge, int MaxAge, Gender Gender);