using MediatR;
using SportyBuddies.Application.Common.DTOs.Buddy;

namespace SportyBuddies.Application.Features.Buddies.Queries.GetUserBuddies;

public record GetUserBuddiesQuery(Guid UserId) : IRequest<List<BuddyResponse>>;