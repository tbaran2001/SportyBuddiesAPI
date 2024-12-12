using MediatR;
using SportyBuddies.Application.Common.DTOs.Match;

namespace SportyBuddies.Application.Features.Matches.Queries.GetProfileMatches;

public record GetProfileMatchesQuery(Guid ProfileId) : IRequest<List<MatchResponse>>;