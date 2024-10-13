using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.DTOs;
using SportyBuddies.Domain.UserAggregate.ValueObjects;

namespace SportyBuddies.Application.Users.Commands.UpdateUser;

public record UpdateUserCommand(UserId UserId, string Name, string Description) : IRequest<ErrorOr<UserResponse>>;