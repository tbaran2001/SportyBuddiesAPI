using MediatR;
using SportyBuddies.Application.Common.DTOs.Conversation;

namespace SportyBuddies.Application.Features.Conversations.Queries.GetLastMessageFromEachUserConversation;

public record GetLastMessageFromEachUserConversationQuery() : IRequest<IEnumerable<MessageResponse>>;