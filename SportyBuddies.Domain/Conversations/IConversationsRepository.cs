namespace SportyBuddies.Domain.Conversations;

public interface IConversationsRepository
{
    Task AddAsync(Conversation conversation);
    Task<Conversation?> GetByIdAsync(Guid id);
    Task AddMessageAsync(Message message);
    Task<Conversation?> GetConversationWithMessagesAsync(Guid conversationId);
    Task<IEnumerable<Message?>> GetLastMessageFromEachUserConversationAsync(Guid userId);
    Task<bool> AreParticipantsBuddiesAsync(ICollection<Guid> participantIds);
    Task<bool> UsersHaveConversation(ICollection<Guid> participantIds);
}