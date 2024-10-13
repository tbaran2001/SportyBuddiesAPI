using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.DTOs;
using SportyBuddies.Domain.UserAggregate.ValueObjects;

namespace SportyBuddies.Application.Matches.Queries.GetRandomMatch;

public record GetRandomMatchQuery(UserId UserId) : IRequest<ErrorOr<MatchResponse?>>;