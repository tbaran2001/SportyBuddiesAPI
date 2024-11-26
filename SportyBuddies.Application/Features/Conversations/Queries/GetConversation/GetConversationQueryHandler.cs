using AutoMapper;
using MediatR;
using SportyBuddies.Application.Common.DTOs.Conversation;
using SportyBuddies.Domain.Common.Interfaces;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Conversations;

namespace SportyBuddies.Application.Features.Conversations.Queries.GetConversation;

public class GetConversationQueryHandler(IConversationsRepository conversationsRepository, IMapper mapper)
    : IRequestHandler<GetConversationQuery, ConversationResponse>
{
    public async Task<ConversationResponse> Handle(GetConversationQuery request, CancellationToken cancellationToken)
    {
        var conversation = await conversationsRepository.GetByIdAsync(request.ConversationId);

        return mapper.Map<ConversationResponse>(conversation);
    }
}