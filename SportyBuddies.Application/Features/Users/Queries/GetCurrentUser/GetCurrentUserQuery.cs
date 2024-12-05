using MediatR;
using SportyBuddies.Application.Common.DTOs.User;

namespace SportyBuddies.Application.Features.Users.Queries.GetCurrentUser;

public record GetCurrentUserQuery : IRequest<UserWithSportsResponse>;