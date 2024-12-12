using AutoMapper;
using MediatR;
using SportyBuddies.Application.Authentication;
using SportyBuddies.Application.Common.DTOs.Conversation;
using SportyBuddies.Domain.Common.Interfaces.Repositories;

namespace SportyBuddies.Application.Features.Conversations.Queries.GetLastMessageFromEachProfileConversation;

public class GetLastMessageFromEachProfileConversationQueryHandler(
    IConversationsRepository conversationsRepository,
    IMapper mapper,
    IUserContext userContext)
    : IRequestHandler<GetLastMessageFromEachProfileConversationQuery, IEnumerable<MessageResponse>>
{
    public async Task<IEnumerable<MessageResponse>> Handle(GetLastMessageFromEachProfileConversationQuery request,
        CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();

        var messages = await conversationsRepository.GetLastMessageFromEachProfileConversationAsync(currentUser.Id);
        return mapper.Map<IEnumerable<MessageResponse>>(messages);
    }
}