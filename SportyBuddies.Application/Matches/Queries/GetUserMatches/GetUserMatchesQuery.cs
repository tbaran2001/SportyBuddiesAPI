using MediatR;

namespace SportyBuddies.Application.Matches.Queries.GetUserMatches;

public record GetUserMatchesQuery(Guid UserId) : IRequest<object>;