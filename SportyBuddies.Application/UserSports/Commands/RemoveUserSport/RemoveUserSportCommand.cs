using ErrorOr;
using MediatR;
using SportyBuddies.Domain.SportAggregate.ValueObjects;
using SportyBuddies.Domain.UserAggregate.ValueObjects;

namespace SportyBuddies.Application.UserSports.Commands.RemoveUserSport;

public record RemoveUserSportCommand(UserId UserId, SportId SportId) : IRequest<ErrorOr<Success>>;