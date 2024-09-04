using MediatR;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.Users.Commands.CreateUser;

public record CreateUserCommand(string Name, string Description) : IRequest<User>;