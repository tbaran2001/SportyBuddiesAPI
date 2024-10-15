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
            .ToListAsync();
    }
}