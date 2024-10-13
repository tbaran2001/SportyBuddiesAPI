using ErrorOr;
using MediatR;
using SportyBuddies.Domain.SportAggregate.ValueObjects;
using SportyBuddies.Domain.UserAggregate.ValueObjects;

namespace SportyBuddies.Application.UserSports.Commands.AddUserSport;

public record AddUserSportCommand(UserId UserId, SportId SportId) : IRequest<ErrorOr<Success>>;