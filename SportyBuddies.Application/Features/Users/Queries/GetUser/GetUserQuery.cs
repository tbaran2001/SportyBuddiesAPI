using MediatR;
using SportyBuddies.Application.Common.DTOs.User;

namespace SportyBuddies.Application.Features.Users.Queries.GetUser;

public record GetUserQuery(Guid UserId) : IRequest<UserWithSportsResponse>;