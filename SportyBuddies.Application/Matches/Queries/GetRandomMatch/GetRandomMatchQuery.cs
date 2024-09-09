using MediatR;
using SportyBuddies.Domain.Matches;

namespace SportyBuddies.Application.Matches.Queries.GetRandomMatch;

public record GetRandomMatchQuery(Guid UserId) : IRequest<Match?>;