using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.DTOs.Sport;

namespace SportyBuddies.Application.Sports.Commands.CreateSport;

public record CreateSportCommand(string Name, string Description) : IRequest<ErrorOr<SportResponse>>;