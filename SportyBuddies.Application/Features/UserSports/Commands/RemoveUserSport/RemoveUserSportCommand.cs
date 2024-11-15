using MediatR;

namespace SportyBuddies.Application.Features.UserSports.Commands.RemoveUserSport;

public record RemoveUserSportCommand(Guid UserId, Guid SportId) : IRequest;