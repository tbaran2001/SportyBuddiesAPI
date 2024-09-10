using MediatR;
using SportyBuddies.Domain.Sports;

namespace SportyBuddies.Application.Sports.Commands.CreateSport;

public record CreateSportCommand(string Name, string Description) : IRequest<Sport>;