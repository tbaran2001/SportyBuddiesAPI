using AutoMapper;
using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.DTOs.Message;
using SportyBuddies.Application.Common.Interfaces;

namespace SportyBuddies.Application.Messages.Queries.GetUserMessagesWithBuddy;

public class GetUserMessagesByBuddyIdQueryHandler(IMessagesRepository messagesRepository, IMapper mapper)
    : IRequestHandler<GetUserMessagesByBuddyIdQuery, ErrorOr<IEnumerable<MessageResponse>>>
{
    public async Task<ErrorOr<IEnumerable<MessageResponse>>> Handle(GetUserMessagesByBuddyIdQuery request,
        CancellationToken cancellationToken)
    {
        var messages = await messagesRepository.GetUserMessagesWithBuddyAsync(request.UserId, request.BuddyId);

        return mapper.Map<List<MessageResponse>>(messages);
    }
}