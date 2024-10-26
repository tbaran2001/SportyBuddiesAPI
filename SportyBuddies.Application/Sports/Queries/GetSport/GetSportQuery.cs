using MediatR;
using SportyBuddies.Application.Common.DTOs.Sport;

namespace SportyBuddies.Application.Sports.Queries.GetSport;

public record GetSportQuery(Guid SportId) : IRequest<SportResponse>;