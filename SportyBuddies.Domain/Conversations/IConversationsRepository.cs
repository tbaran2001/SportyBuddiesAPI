namespace SportyBuddies.Domain.Conversations;

public interface IConversationsRepository
{
    Task AddAsync(Conversation conversation);
    Task<Conversation?> GetByIdAsync(Guid id);
    Task AddMessageAsync(Message message);
}