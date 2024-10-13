using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.DTOs;
using SportyBuddies.Domain.UserAggregate.ValueObjects;

namespace SportyBuddies.Application.UserSports.Queries.GetUserSports;

public record GetUserSportsQuery(UserId UserId) : IRequest<ErrorOr<List<SportResponse>>>;