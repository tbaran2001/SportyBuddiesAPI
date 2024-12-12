using MediatR;
using SportyBuddies.Application.Common.DTOs.Sport;

namespace SportyBuddies.Application.Features.ProfileSports.Queries.GetProfileSports;

public record GetProfileSportsQuery : IRequest<List<SportResponse>>;