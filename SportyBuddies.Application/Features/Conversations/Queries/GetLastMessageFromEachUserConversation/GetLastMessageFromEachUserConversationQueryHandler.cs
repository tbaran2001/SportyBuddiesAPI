using AutoMapper;
using MediatR;
using SportyBuddies.Application.Authentication;
using SportyBuddies.Application.Common.DTOs.Conversation;
using SportyBuddies.Domain.Common.Interfaces.Repositories;

namespace SportyBuddies.Application.Features.Conversations.Queries.GetLastMessageFromEachUserConversation;

public class GetLastMessageFromEachUserConversationQueryHandler(
    IConversationsRepository conversationsRepository,
    IMapper mapper,
    IUserContext userContext)
    : IRequestHandler<GetLastMessageFromEachUserConversationQuery, IEnumerable<MessageResponse>>
{
    public async Task<IEnumerable<MessageResponse>> Handle(GetLastMessageFromEachUserConversationQuery request,
        CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();

        var messages = await conversationsRepository.GetLastMessageFromEachUserConversationAsync(currentUser.Id);
        return mapper.Map<IEnumerable<MessageResponse>>(messages);
    }
}