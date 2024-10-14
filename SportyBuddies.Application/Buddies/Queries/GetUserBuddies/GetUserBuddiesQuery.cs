using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.DTOs;

namespace SportyBuddies.Application.Buddies.Queries.GetUserBuddies;

public record GetUserBuddiesQuery(Guid UserId) : IRequest<ErrorOr<List<BuddyResponse>>>;