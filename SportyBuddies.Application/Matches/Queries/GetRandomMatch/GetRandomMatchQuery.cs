using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.DTOs;

namespace SportyBuddies.Application.Matches.Queries.GetRandomMatch;

public record GetRandomMatchQuery(Guid UserId) : IRequest<ErrorOr<MatchResponse?>>;