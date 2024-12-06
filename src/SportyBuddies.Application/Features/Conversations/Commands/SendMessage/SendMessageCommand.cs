using MediatR;
using SportyBuddies.Application.Common.DTOs.Conversation;

namespace SportyBuddies.Application.Features.Conversations.Commands.SendMessage;

public record SendMessageCommand(Guid ConversationId, string Content):IRequest<MessageResponse>;