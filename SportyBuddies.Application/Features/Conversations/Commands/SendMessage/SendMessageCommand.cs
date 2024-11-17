using MediatR;
using SportyBuddies.Application.Common.DTOs.Message;
using SportyBuddies.Domain.Conversations;

namespace SportyBuddies.Application.Features.Conversations.Commands.SendMessage;

public record SendMessageCommand(Guid UserId, Guid ConversationId, string Content):IRequest<MessageResponse>;