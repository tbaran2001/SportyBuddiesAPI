using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.DTOs;

namespace SportyBuddies.Application.Users.Queries.GetUsers;

public record GetUsersQuery : IRequest<ErrorOr<List<UserResponse>>>;