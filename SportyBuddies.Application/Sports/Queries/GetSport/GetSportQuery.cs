using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.DTOs;
using SportyBuddies.Domain.SportAggregate.ValueObjects;

namespace SportyBuddies.Application.Sports.Queries.GetSport;

public record GetSportQuery(SportId SportId) : IRequest<ErrorOr<SportResponse>>;