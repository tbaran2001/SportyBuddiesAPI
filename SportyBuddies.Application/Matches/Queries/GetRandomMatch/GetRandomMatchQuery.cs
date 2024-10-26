using MediatR;
using SportyBuddies.Application.Common.DTOs.Message;

namespace SportyBuddies.Application.Matches.Queries.GetRandomMatch;

public record GetRandomMatchQuery(Guid UserId) : IRequest<RandomMatchResponse?>;