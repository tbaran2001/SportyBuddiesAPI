using MediatR;
using ErrorOr;
using SportyBuddies.Domain.Sports;

namespace SportyBuddies.Application.Sports.Commands;

public record CreateSportCommand(string SportType, string Name, string Description, Guid AdminId) : IRequest<ErrorOr<Sport>>;