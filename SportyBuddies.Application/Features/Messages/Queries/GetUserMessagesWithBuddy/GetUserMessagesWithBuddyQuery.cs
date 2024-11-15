using MediatR;
using SportyBuddies.Application.Common.DTOs.Message;

namespace SportyBuddies.Application.Features.Messages.Queries.GetUserMessagesWithBuddy;

public record GetUserMessagesWithBuddyQuery(Guid UserId, Guid BuddyId)
    : IRequest<List<MessageResponse>>;