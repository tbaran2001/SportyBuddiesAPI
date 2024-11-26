using SportyBuddies.Domain.Conversations;

namespace SportyBuddies.Domain.Common.Interfaces.Services;

public interface IConversationService
{
    Task<Conversation> CreateConversationAsync(Guid creatorId, Guid participantId);
}