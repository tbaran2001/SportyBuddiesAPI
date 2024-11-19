using Microsoft.EntityFrameworkCore;
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
            .Select(g => g.OrderByDescending(m => m.CreatedAt).FirstOrDefault())
            .ToListAsync();
    }

    public async Task<bool> AreParticipantsBuddiesAsync(ICollection<Guid> participantIds)
    {
        var first = participantIds.First();
        var last = participantIds.Last();
        return await dbContext.Buddies
            .AnyAsync(b =>
                b.UserId == first && b.MatchedUserId == last || b.UserId == last && b.MatchedUserId == first);
    }

    public async Task<bool> UsersHaveConversation(ICollection<Guid> participantIds)
    {
        var conversations= await dbContext.Conversations
            .Include(c=>c.Participants)
            .Where(c => c.CreatorId == participantIds.First() ||
                        c.Participants.Any(p => p.UserId == participantIds.First()))
            .ToListAsync();

        return conversations.Any(c =>
            c.Participants.Select(p => p.UserId).OrderBy(id => id)
                .SequenceEqual(participantIds.OrderBy(id => id)));
    }

    public async Task<bool> ExistsAsync(Guid conversationId)
    {
        return await dbContext.Conversations
            .AnyAsync(c => c.Id == conversationId);
    }
}