using MediatR;
using SportyBuddies.Domain.Matches;

namespace SportyBuddies.Application.Matches.Queries.GetUserMatches;

public record GetUserMatchesQuery(Guid UserId) : IRequest<IEnumerable<Match>>;