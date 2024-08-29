using ErrorOr;
using MediatR;
using SportyBuddies.Domain.Sports;

namespace SportyBuddies.Application.Sports.Commands;

public record CreateSportCommand(string Name, string Description) : IRequest<ErrorOr<Sport>>;