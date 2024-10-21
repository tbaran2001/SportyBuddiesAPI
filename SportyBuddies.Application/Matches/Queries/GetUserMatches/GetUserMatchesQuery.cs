using ErrorOr;
using MediatR;

namespace SportyBuddies.Application.Matches.Queries.GetUserMatches;

public record GetUserMatchesQuery(Guid UserId, bool IncludeUsers) : IRequest<ErrorOr<object>>;