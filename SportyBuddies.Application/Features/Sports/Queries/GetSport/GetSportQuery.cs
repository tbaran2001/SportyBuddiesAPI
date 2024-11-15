using MediatR;
using SportyBuddies.Application.Common.DTOs.Sport;

namespace SportyBuddies.Application.Features.Sports.Queries.GetSport;

public record GetSportQuery(Guid SportId) : IRequest<SportResponse>;