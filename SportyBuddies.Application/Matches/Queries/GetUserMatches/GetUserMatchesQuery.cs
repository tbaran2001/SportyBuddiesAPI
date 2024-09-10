using MediatR;
using SportyBuddies.Application.Common.DTOs;

namespace SportyBuddies.Application.Matches.Queries.GetUserMatches;

public record GetUserMatchesQuery(Guid UserId) : IRequest<IEnumerable<MatchDto>>;