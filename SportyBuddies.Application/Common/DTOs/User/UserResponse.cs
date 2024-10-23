using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.Common.DTOs.User;

public record UserResponse(
    Guid Id,
    string Name,
    string Description,
    DateTime LastActive,
    Guid? MainPhotoId,
    Gender? Gender);