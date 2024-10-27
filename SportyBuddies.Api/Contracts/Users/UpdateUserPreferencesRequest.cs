using SportyBuddies.Domain.Users;

namespace SportyBuddies.Api.Contracts.Users;

public record UpdateUserPreferencesRequest(int MinAge, int MaxAge, Gender Gender);