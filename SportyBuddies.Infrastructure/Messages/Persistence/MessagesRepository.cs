using Microsoft.EntityFrameworkCore;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Domain.Messages;
using SportyBuddies.Infrastructure.Common.Persistence;

namespace SportyBuddies.Infrastructure.Messages.Persistence;

public class MessagesRepository(SportyBuddiesDbContext dbContext) : IMessagesRepository
{
    public async Task AddMessageAsync(Message message)
    {
        await dbContext.Messages.AddAsync(message);
    }

    public async Task<IEnumerable<Message>> GetUserMessagesAsync(Guid userId)
    {
        return await dbContext.Messages
            .Where(m => m.Sender.Id == userId || m.Recipient.Id == userId)
            .Include(m => m.Sender)
            .Include(m => m.Recipient)
            .OrderBy(m => m.TimeSent)
            .ToListAsync();
    }

    public async Task<IEnumerable<Message>> GetUserMessagesWithBuddyAsync(Guid userId, Guid buddyId)
    {
        return await dbContext.Messages
            .Where(m => (m.Sender.Id == userId && m.Recipient.Id == buddyId) ||
                        (m.Sender.Id == buddyId && m.Recipient.Id == userId))
            .Include(m => m.Sender)
            .Include(m => m.Recipient)
            .OrderBy(m => m.TimeSent)
            .ToListAsync();
    }

    public async Task<IEnumerable<Message>> GetLastUserMessagesAsync(Guid userId)
    {
        return await dbContext.Messages
            .Where(m => m.Sender.Id == userId || m.Recipient.Id == userId)
            .Include(m => m.Sender)
            .Include(m => m.Recipient)
            .GroupBy(m => m.Sender.Id == userId ? m.Recipient.Id : m.Sender.Id)
            .Select(g => g.OrderByDescending(m => m.TimeSent).First())
            .ToListAsync();
    }
}