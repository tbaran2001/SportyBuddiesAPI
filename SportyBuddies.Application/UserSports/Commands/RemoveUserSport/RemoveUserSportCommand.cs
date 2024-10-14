using ErrorOr;
using MediatR;

namespace SportyBuddies.Application.UserSports.Commands.RemoveUserSport;

public record RemoveUserSportCommand(Guid UserId, Guid SportId) : IRequest<ErrorOr<Success>>;