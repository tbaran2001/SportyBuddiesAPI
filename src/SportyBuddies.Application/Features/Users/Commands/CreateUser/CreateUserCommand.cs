using MediatR;
using SportyBuddies.Application.Common.DTOs.User;

namespace SportyBuddies.Application.Features.Users.Commands.CreateUser;

public record CreateUserCommand(string Name, string Description) : IRequest<UserWithSportsResponse>;