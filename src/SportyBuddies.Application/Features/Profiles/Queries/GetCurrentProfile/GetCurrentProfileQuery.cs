using MediatR;
using SportyBuddies.Application.Common.DTOs.Profile;

namespace SportyBuddies.Application.Features.Profiles.Queries.GetCurrentProfile;

public record GetCurrentProfileQuery : IRequest<ProfileWithSportsResponse>;