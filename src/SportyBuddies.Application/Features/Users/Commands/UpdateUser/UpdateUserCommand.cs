using MediatR;
using SportyBuddies.Application.Common.DTOs.User;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.Features.Users.Commands.UpdateUser;

public record UpdateUserCommand(string Name, string Description, Gender Gender, DateOnly DateOfBirth)
    : IRequest<UserResponse>;