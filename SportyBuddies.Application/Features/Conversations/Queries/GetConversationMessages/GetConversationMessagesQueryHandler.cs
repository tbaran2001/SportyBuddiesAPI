using AutoMapper;
using MediatR;
using SportyBuddies.Application.Common.DTOs.Message;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Domain.Conversations;

namespace SportyBuddies.Application.Features.Conversations.Queries.GetConversationMessages;

public class GetConversationMessagesQueryHandler(IConversationsRepository conversationsRepository, IMapper mapper)
    : IRequestHandler<GetConversationMessagesQuery, IEnumerable<MessageResponse>>
{
    public async Task<IEnumerable<MessageResponse>> Handle(GetConversationMessagesQuery request,
        CancellationToken cancellationToken)
    {
        var conversation = await conversationsRepository.GetConversationWithMessagesAsync(request.ConversationId);
        if (conversation == null)
            throw new NotFoundException(nameof(Conversation), request.ConversationId.ToString());

        return mapper.Map<IEnumerable<MessageResponse>>(conversation.Messages);
    }
}