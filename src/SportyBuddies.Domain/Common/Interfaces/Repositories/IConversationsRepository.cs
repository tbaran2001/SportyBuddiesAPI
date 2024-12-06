using SportyBuddies.Domain.Conversations;

namespace SportyBuddies.Domain.Common.Interfaces.Repositories;

public interface IConversationsRepository
{
    Task AddAsync(Conversation conversation);
    Task<Conversation?> GetByIdAsync(Guid id);
    Task AddMessageAsync(Message message);
    Task<Conversation?> GetConversationWithMessagesAsync(Guid conversationId);
    Task<IEnumerable<Message?>> GetLastMessageFromEachUserConversationAsync(Guid userId);
    Task<bool> UsersHaveConversationAsync(Guid firstUserId, Guid secondUserId);
}