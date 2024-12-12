using MediatR;
using SportyBuddies.Application.Common.DTOs.Buddy;

namespace SportyBuddies.Application.Features.Buddies.Queries.GetProfileBuddies;

public record GetProfileBuddiesQuery : IRequest<List<BuddyResponse>>;