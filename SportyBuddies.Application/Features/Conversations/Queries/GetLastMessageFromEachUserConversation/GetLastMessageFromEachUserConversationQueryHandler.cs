using AutoMapper;
using MediatR;
using SportyBuddies.Application.Common.DTOs.Message;
using SportyBuddies.Domain.Conversations;

namespace SportyBuddies.Application.Features.Conversations.Queries.GetLastMessageFromEachUserConversation;

public class GetLastMessageFromEachUserConversationQueryHandler(
    IConversationsRepository conversationsRepository,
    IMapper mapper) : IRequestHandler<GetLastMessageFromEachUserConversationQuery, IEnumerable<MessageResponse>>
{
    public async Task<IEnumerable<MessageResponse>> Handle(GetLastMessageFromEachUserConversationQuery request,
        CancellationToken cancellationToken)
    {
        var messages = await conversationsRepository.GetLastMessageFromEachUserConversationAsync(request.UserId);
        return mapper.Map<IEnumerable<MessageResponse>>(messages);
    }
}