using ErrorOr;
using MediatR;
using SportyBuddies.Domain.UserAggregate.ValueObjects;

namespace SportyBuddies.Application.Users.Commands.DeleteUser;

public record DeleteUserCommand(UserId UserId) : IRequest<ErrorOr<Deleted>>;