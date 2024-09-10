using MediatR;
using SportyBuddies.Application.Common.DTOs;

namespace SportyBuddies.Application.Users.Queries.GetUsers;

public record GetUsersQuery() : IRequest<IEnumerable<UserDto>>;