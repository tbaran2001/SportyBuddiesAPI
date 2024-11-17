using MediatR;
using SportyBuddies.Application.Common.DTOs.Conversation;

namespace SportyBuddies.Application.Features.Conversations.Commands.CreateConversation;

public record CreateConversationCommand(Guid CreatorId, ICollection<Guid> ParticipantIds) : IRequest<ConversationResponse>;