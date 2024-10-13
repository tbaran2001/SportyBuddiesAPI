using ErrorOr;
using MediatR;
using SportyBuddies.Domain.SportAggregate.ValueObjects;

namespace SportyBuddies.Application.Sports.Commands.DeleteSport;

public record DeleteSportCommand(SportId SportId) : IRequest<ErrorOr<Deleted>>;