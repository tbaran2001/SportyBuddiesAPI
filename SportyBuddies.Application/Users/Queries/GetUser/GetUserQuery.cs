using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.DTOs;
using SportyBuddies.Domain.UserAggregate.ValueObjects;

namespace SportyBuddies.Application.Users.Queries.GetUser;

public record GetUserQuery(UserId UserId) : IRequest<ErrorOr<UserResponse>>;