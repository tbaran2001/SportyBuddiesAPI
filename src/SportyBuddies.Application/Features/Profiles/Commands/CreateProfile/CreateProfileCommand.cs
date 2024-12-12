using MediatR;
using SportyBuddies.Application.Common.DTOs.Profile;

namespace SportyBuddies.Application.Features.Profiles.Commands.CreateProfile;

public record CreateProfileCommand(string Name, string Description) : IRequest<ProfileWithSportsResponse>;