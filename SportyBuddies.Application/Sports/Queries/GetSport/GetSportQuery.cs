using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.DTOs;

namespace SportyBuddies.Application.Sports.Queries.GetSport;

public record GetSportQuery(Guid SportId) : IRequest<ErrorOr<SportDto>>;