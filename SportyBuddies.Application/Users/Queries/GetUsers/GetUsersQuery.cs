using ErrorOr;
using MediatR;

namespace SportyBuddies.Application.Users.Queries.GetUsers;

public record GetUsersQuery(bool IncludeSports) : IRequest<ErrorOr<object>>;