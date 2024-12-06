using MediatR;
using SportyBuddies.Application.Common.DTOs.Sport;

namespace SportyBuddies.Application.Features.UserSports.Queries.GetUserSports;

public record GetUserSportsQuery : IRequest<List<SportResponse>>;