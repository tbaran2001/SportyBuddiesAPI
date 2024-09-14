using ErrorOr;
using MediatR;

namespace SportyBuddies.Application.Sports.Commands.DeleteSport;

public record DeleteSportCommand(Guid SportId) : IRequest<ErrorOr<Deleted>>;