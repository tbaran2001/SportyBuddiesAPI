using MediatR;
using SportyBuddies.Application.Common.DTOs.Match;

namespace SportyBuddies.Application.Features.Matches.Queries.GetUserMatches;

public record GetUserMatchesQuery(Guid UserId) : IRequest<List<MatchResponse>>;