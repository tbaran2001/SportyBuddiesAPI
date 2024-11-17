using Microsoft.EntityFrameworkCore;
using SportyBuddies.Domain.Conversations;

namespace SportyBuddies.Infrastructure.Repositories;

public class ConversationsRepository(SportyBuddiesDbContext dbContext): IConversationsRepository
{
    public async Task AddAsync(Conversation conversation)
    {
        await dbContext.Conversations.AddAsync(conversation);
    }

    public async Task<Conversation?> GetByIdAsync(Guid id)
    {
        return await dbContext.Conversations
            .FirstOrDefaultAsync(c=>c.Id==id);
    }

    public async Task AddMessageAsync(Message message)
    {
        await dbContext.Messages.AddAsync(message);
    }
}