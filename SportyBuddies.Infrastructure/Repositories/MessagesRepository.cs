using Microsoft.EntityFrameworkCore;
using SportyBuddies.Domain.Messages;

namespace SportyBuddies.Infrastructure.Repositories;

public class MessagesRepository(SportyBuddiesDbContext dbContext) : IMessagesRepository
{
    public async Task AddMessageAsync(Message message)
    {
        await dbContext.Messages.AddAsync(message);
    }

    public async Task<IEnumerable<Message>> GetUserMessagesAsync(Guid userId)
    {
        return await dbContext.Messages
            .Where(m => m.SenderId == userId || m.RecipientId == userId)
            .OrderBy(m => m.TimeSent)
            .ToListAsync();
    }

    public async Task<IEnumerable<Message>> GetUserMessagesWithBuddyAsync(Guid userId, Guid buddyId)
    {
        return await dbContext.Messages
            .Where(m => (m.SenderId == userId && m.RecipientId == buddyId) ||
                        (m.SenderId == buddyId && m.RecipientId == userId))
            .OrderBy(m => m.TimeSent)
            .ToListAsync();
    }

    public async Task<IEnumerable<Message>> GetLastUserMessagesAsync(Guid userId)
    {
        return await dbContext.Messages
            .Where(m => m.SenderId == userId || m.RecipientId == userId)
            .GroupBy(m => m.SenderId == userId ? m.RecipientId : m.SenderId)
            .Select(g => g.OrderByDescending(m => m.TimeSent).First())
            .ToListAsync();
    }

    public async Task RemoveUserMessagesAsync(Guid userId)
    {
        await dbContext.Messages
            .Where(m => m.SenderId == userId || m.RecipientId == userId)
            .ExecuteDeleteAsync();
    }
}