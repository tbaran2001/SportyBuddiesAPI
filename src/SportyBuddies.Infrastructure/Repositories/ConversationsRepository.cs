using Microsoft.EntityFrameworkCore;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Conversations;

namespace SportyBuddies.Infrastructure.Repositories;

public class ConversationsRepository(SportyBuddiesDbContext dbContext) : IConversationsRepository
{
    public async Task AddAsync(Conversation conversation)
    {
        await dbContext.Conversations.AddAsync(conversation);
    }

    public async Task<Conversation?> GetByIdAsync(Guid id)
    {
        return await dbContext.Conversations
            .Include(c=>c.Participants)
            .ThenInclude(p=>p.User)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task AddMessageAsync(Message message)
    {
        await dbContext.Messages.AddAsync(message);
    }

    public async Task<Conversation?> GetConversationWithMessagesAsync(Guid conversationId)
    {
        return await dbContext.Conversations
            .Include(c => c.Messages)
            .FirstOrDefaultAsync(c => c.Id == conversationId);
    }

    public async Task<IEnumerable<Message?>> GetLastMessageFromEachUserConversationAsync(Guid userId)
    {
        return await dbContext.Conversations
            .Where(c => c.Participants.Any(p => p.UserId == userId))
            .SelectMany(c => c.Messages)
            .GroupBy(m => m.ConversationId)
            .Select(g => g.OrderByDescending(m => m.CreatedOnUtc).FirstOrDefault())
            .ToListAsync();
    }

    public async Task<bool> UsersHaveConversationAsync(Guid firstUserId, Guid secondUserId)
    {
        return await dbContext.Conversations
            .Where(c => c.Participants.Any(p => p.UserId == firstUserId))
            .Where(c => c.Participants.Any(p => p.UserId == secondUserId))
            .AnyAsync();
    }
}