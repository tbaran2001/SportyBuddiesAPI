using MediatR;
using SportyBuddies.Application.Common.DTOs.Sport;

namespace SportyBuddies.Application.Features.Sports.Commands.CreateSport;

public record CreateSportCommand(string Name, string Description) : IRequest<SportResponse>;