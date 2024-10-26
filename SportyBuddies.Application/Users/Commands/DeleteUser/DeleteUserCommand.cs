using MediatR;

namespace SportyBuddies.Application.Users.Commands.DeleteUser;

public record DeleteUserCommand(Guid UserId) : IRequest;