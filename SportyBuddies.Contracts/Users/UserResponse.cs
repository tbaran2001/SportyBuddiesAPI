using SportyBuddies.Contracts.Sports;

namespace SportyBuddies.Contracts.Users;

public record UserResponse(
    Guid Id,
    string Name,
    string Description,
    DateTime LastActive,
    ICollection<SportResponse> Sports);