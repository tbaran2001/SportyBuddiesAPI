using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.DTOs.User;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.Users.Commands.UpdateUser;

public record UpdateUserCommand(Guid UserId, string Name, string Description, Gender Gender)
    : IRequest<ErrorOr<UserWithSportsResponse>>;