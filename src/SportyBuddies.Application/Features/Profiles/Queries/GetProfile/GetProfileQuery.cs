using MediatR;
using SportyBuddies.Application.Common.DTOs.Profile;

namespace SportyBuddies.Application.Features.Profiles.Queries.GetProfile;

public record GetProfileQuery(Guid ProfileId) : IRequest<ProfileWithSportsResponse>;