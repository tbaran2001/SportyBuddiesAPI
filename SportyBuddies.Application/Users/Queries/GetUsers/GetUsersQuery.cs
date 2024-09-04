using MediatR;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.Users.Queries.GetUsers;

public record GetUsersQuery():IRequest<IEnumerable<User>>;