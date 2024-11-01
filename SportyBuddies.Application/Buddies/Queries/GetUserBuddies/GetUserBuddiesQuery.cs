using MediatR;

namespace SportyBuddies.Application.Buddies.Queries.GetUserBuddies;

public record GetUserBuddiesQuery(Guid UserId) : IRequest<object>;