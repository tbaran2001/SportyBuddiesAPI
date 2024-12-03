using MediatR;
using SportyBuddies.Application.Common.DTOs.Match;

namespace SportyBuddies.Application.Features.Matches.Queries.GetRandomMatch;

public record GetRandomMatchQuery() : IRequest<RandomMatchResponse?>;