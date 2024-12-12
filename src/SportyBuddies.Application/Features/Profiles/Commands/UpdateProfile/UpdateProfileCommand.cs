using MediatR;
using SportyBuddies.Application.Common.DTOs.Profile;
using SportyBuddies.Domain.Profiles;

namespace SportyBuddies.Application.Features.Profiles.Commands.UpdateProfile;

public record UpdateProfileCommand(string Name, string Description, Gender Gender, DateOnly DateOfBirth)
    : IRequest<ProfileResponse>;