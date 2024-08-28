using MediatR;

namespace SportyBuddies.Application.Sports.Commands;

public record CreateSportCommand(string SportType, string Name, string Description, Guid AdminId) : IRequest<Guid>;