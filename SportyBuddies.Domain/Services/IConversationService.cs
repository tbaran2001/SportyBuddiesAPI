using SportyBuddies.Domain.Conversations;

namespace SportyBuddies.Domain.Services;

public interface IConversationService
{
    Task<Conversation> CreateConversationAsync(Guid creatorId, Guid participantId);
}