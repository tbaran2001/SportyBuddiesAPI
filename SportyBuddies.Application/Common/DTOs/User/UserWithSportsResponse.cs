using SportyBuddies.Application.Common.DTOs.Sport;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.Common.DTOs.User;

public record UserWithSportsResponse(
    Guid Id,
    string Name,
    string Description,
    DateTime CreatedOnUtc,
    Guid MainPhotoId,
    Gender Gender,
    DateOnly DateOfBirth,
    Preferences Preferences,
    ICollection<SportResponse> Sports);