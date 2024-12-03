using MediatR;
using SportyBuddies.Application.Common.DTOs.Conversation;
using SportyBuddies.Domain.Conversations;

namespace SportyBuddies.Application.Features.Conversations.Commands.SendMessage;

public record SendMessageCommand(Guid ConversationId, string Content):IRequest<MessageResponse>;