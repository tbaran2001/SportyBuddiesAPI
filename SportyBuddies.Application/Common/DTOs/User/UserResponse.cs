using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.Common.DTOs.User;

public record UserResponse(
    Guid Id,
    string Name,
    string Description,
    DateTime CreatedOnUtc,
    Guid MainPhotoId,
    Gender Gender,
    DateOnly DateOfBirth,
    Preferences Preferences);