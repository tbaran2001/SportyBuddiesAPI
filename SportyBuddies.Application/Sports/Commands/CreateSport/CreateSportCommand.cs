using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.DTOs;

namespace SportyBuddies.Application.Sports.Commands.CreateSport;

public record CreateSportCommand(string Name, string Description) : IRequest<ErrorOr<SportDto>>;