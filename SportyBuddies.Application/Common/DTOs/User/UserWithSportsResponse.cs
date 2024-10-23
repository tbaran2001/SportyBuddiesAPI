using SportyBuddies.Application.Common.DTOs.Sport;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.Common.DTOs.User;

public record UserWithSportsResponse(
    Guid Id,
    string Name,
    string Description,
    DateTime LastActive,
    Guid? MainPhotoId,
    Gender? Gender,
    ICollection<SportResponse> Sports);