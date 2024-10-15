using AutoMapper;
using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.DTOs;
using SportyBuddies.Application.Common.Interfaces;

namespace SportyBuddies.Application.Messages.Queries.GetUserMessagesWithBuddy;

public class GetUserMessagesWithBuddyQueryHandler(IMessagesRepository messagesRepository, IMapper mapper)
    : IRequestHandler<GetUserMessagesWithBuddyQuery, ErrorOr<IEnumerable<MessageResponse>>>
{
    public async Task<ErrorOr<IEnumerable<MessageResponse>>> Handle(GetUserMessagesWithBuddyQuery request,
        CancellationToken cancellationToken)
    {
        var messages = await messagesRepository.GetUserMessagesWithBuddyAsync(request.UserId, request.BuddyId);

        return mapper.Map<List<MessageResponse>>(messages);
    }
}