using MediatR;

namespace SportyBuddies.Application.Features.Sports.Commands.DeleteSport;

public record DeleteSportCommand(Guid SportId) : IRequest;