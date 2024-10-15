using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.DTOs;

namespace SportyBuddies.Application.Messages.Queries.GetUserMessagesWithBuddy;

public record GetUserMessagesWithBuddyQuery(Guid UserId, Guid BuddyId)
    : IRequest<ErrorOr<IEnumerable<MessageResponse>>>;