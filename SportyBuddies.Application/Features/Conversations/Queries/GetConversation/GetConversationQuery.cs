using MediatR;
using SportyBuddies.Application.Common.DTOs.Conversation;

namespace SportyBuddies.Application.Features.Conversations.Queries.GetConversation;

public record GetConversationQuery(Guid ConversationId) : IRequest<ConversationResponse>;