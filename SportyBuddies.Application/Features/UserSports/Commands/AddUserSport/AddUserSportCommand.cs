using MediatR;

namespace SportyBuddies.Application.Features.UserSports.Commands.AddUserSport;

public record AddUserSportCommand(Guid UserId, Guid SportId) : IRequest;