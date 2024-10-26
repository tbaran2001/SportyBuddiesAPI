using MediatR;

namespace SportyBuddies.Application.Buddies.Queries.GetUserBuddies;

public record GetUserBuddiesQuery(Guid UserId, bool IncludeUsers) : IRequest<object>;