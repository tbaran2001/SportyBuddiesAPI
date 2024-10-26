using MediatR;

namespace SportyBuddies.Application.UserSports.Commands.AddUserSport;

public record AddUserSportCommand(Guid UserId, Guid SportId) : IRequest;