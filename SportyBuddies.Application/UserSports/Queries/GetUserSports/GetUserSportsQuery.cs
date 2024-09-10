using MediatR;
using SportyBuddies.Domain.Sports;

namespace SportyBuddies.Application.UserSports.Queries.GetUserSports;

public record GetUserSportsQuery(Guid UserId) : IRequest<IEnumerable<Sport>>;