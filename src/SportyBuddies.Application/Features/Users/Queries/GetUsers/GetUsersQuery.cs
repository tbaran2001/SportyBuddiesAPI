using MediatR;
using SportyBuddies.Application.Common.DTOs.User;

namespace SportyBuddies.Application.Features.Users.Queries.GetUsers;

public record GetUsersQuery : IRequest<List<UserWithSportsResponse>>;