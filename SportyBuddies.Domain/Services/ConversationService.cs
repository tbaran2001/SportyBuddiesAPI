using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Common.Interfaces.Services;
using SportyBuddies.Domain.Conversations;

namespace SportyBuddies.Domain.Services;

public class ConversationService(IBuddiesRepository buddiesRepository, IConversationsRepository conversationsRepository)
    : IConversationService
{
    public async Task<Conversation> CreateConversationAsync(Guid creatorId, Guid participantId)
    {
        if (!await buddiesRepository.AreUsersAlreadyBuddiesAsync(creatorId, participantId))
        {
            throw new InvalidOperationException("Participants are not buddies.");
        }

        if (await conversationsRepository.UsersHaveConversationAsync(creatorId, participantId))
        {
            throw new InvalidOperationException("Conversation already exists.");
        }

        var conversation = Conversation.CreateOneToOne(creatorId, participantId);

        var buddies = await buddiesRepository.GetRelatedBuddies(creatorId, participantId);
        foreach (var buddy in buddies)
        {
            buddy.SetConversation(conversation);
        }

        await conversationsRepository.AddAsync(conversation);

        return conversation;
    }
}