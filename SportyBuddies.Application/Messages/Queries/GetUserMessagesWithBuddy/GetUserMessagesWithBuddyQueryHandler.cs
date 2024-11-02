using AutoMapper;
using MediatR;
using SportyBuddies.Application.Common.DTOs.Message;
using SportyBuddies.Domain.Messages;

namespace SportyBuddies.Application.Messages.Queries.GetUserMessagesWithBuddy;

public class GetUserMessagesWithBuddyQueryHandler(IMessagesRepository messagesRepository, IMapper mapper)
    : IRequestHandler<GetUserMessagesWithBuddyQuery, IEnumerable<MessageResponse>>
{
    public async Task<IEnumerable<MessageResponse>> Handle(GetUserMessagesWithBuddyQuery request,
        CancellationToken cancellationToken)
    {
        var messages = await messagesRepository.GetUserMessagesWithBuddyAsync(request.UserId, request.BuddyId);

        return mapper.Map<List<MessageResponse>>(messages);
    }
}