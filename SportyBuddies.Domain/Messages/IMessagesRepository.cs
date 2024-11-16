namespace SportyBuddies.Domain.Messages;

public interface IMessagesRepository
{
    Task AddMessageAsync(Message message);
    Task<IEnumerable<Message>> GetUserMessagesAsync(Guid userId);
    Task<IEnumerable<Message>> GetUserMessagesWithBuddyAsync(Guid userId, Guid buddyId);
    Task<IEnumerable<Message>> GetLastUserMessagesAsync(Guid userId);
    Task RemoveUserMessagesAsync(Guid userId);
}