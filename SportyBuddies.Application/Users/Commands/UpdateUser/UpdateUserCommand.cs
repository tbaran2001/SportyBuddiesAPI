using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.DTOs;

namespace SportyBuddies.Application.Users.Commands.UpdateUser;

public record UpdateUserCommand(Guid UserId, string Name, string Description) : IRequest<ErrorOr<UserDto>>;