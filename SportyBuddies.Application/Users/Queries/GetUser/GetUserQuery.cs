using MediatR;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.Users.Queries.GetUser;

public record GetUserQuery(Guid UserId) : IRequest<User>;