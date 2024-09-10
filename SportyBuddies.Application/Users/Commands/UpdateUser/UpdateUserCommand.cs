using MediatR;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.Users.Commands.UpdateUser;

public record UpdateUserCommand(Guid UserId, string Name, string Description) : IRequest<User>;