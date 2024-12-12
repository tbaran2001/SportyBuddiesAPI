using MediatR;
using SportyBuddies.Application.Common.DTOs.Conversation;

namespace SportyBuddies.Application.Features.Conversations.Queries.GetLastMessageFromEachProfileConversation;

public record GetLastMessageFromEachProfileConversationQuery : IRequest<IEnumerable<MessageResponse>>;