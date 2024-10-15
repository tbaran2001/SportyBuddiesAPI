using SportyBuddies.Domain.Messages;

namespace SportyBuddies.Application.Common.Interfaces;

public interface IMessagesRepository
{
    Task AddMessageAsync(Message message);
    Task<IEnumerable<Message>> GetUserMessagesAsync(Guid userId);
}