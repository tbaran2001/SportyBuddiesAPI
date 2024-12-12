using MediatR;
using SportyBuddies.Application.Common.DTOs.Profile;

namespace SportyBuddies.Application.Features.Profiles.Queries.GetProfiles;

public record GetProfilesQuery : IRequest<List<ProfileWithSportsResponse>>;