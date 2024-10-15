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
}