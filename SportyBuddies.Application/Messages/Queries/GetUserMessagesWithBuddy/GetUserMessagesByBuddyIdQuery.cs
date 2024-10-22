using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.DTOs.Message;

namespace SportyBuddies.Application.Messages.Queries.GetUserMessagesWithBuddy;

public record GetUserMessagesByBuddyIdQuery(Guid UserId, Guid BuddyId)
    : IRequest<ErrorOr<IEnumerable<MessageResponse>>>;