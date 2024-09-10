using MediatR;
using SportyBuddies.Application.Common.DTOs;

namespace SportyBuddies.Application.UserSports.Queries.GetUserSports;

public record GetUserSportsQuery(Guid UserId) : IRequest<IEnumerable<SportDto>>;