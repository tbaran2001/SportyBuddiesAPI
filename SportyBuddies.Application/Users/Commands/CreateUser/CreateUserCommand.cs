using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.DTOs;

namespace SportyBuddies.Application.Users.Commands.CreateUser;

public record CreateUserCommand(string Name, string Description) : IRequest<ErrorOr<UserWithSportsResponse>>;