using SportyBuddies.Application.Common.DTOs.Sport;

namespace SportyBuddies.Application.Common.DTOs.User;

public record UserWithSportsResponse(
    Guid Id,
    string Name,
    string Description,
    DateTime LastActive,
    ICollection<SportResponse> Sports);