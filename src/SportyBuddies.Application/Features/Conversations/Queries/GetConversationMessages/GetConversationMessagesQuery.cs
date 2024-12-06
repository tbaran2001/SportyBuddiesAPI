using MediatR;
using SportyBuddies.Application.Common.DTOs.Conversation;

namespace SportyBuddies.Application.Features.Conversations.Queries.GetConversationMessages;

public record GetConversationMessagesQuery(Guid ConversationId) : IRequest<IEnumerable<MessageResponse>>;