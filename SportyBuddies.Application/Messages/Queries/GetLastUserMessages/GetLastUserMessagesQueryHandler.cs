using AutoMapper;
using MediatR;
using SportyBuddies.Application.Common.DTOs.Message;
using SportyBuddies.Application.Common.Interfaces;

namespace SportyBuddies.Application.Messages.Queries.GetLastUserMessages;

public class GetLastUserMessagesQueryHandler(IMessagesRepository messagesRepository, IMapper mapper)
    : IRequestHandler<GetLastUserMessagesQuery, IEnumerable<MessageResponse>>
{
    public async Task<IEnumerable<MessageResponse>> Handle(GetLastUserMessagesQuery request,
        CancellationToken cancellationToken)
    {
        var messages = await messagesRepository.GetLastUserMessagesAsync(request.UserId);

        return mapper.Map<List<MessageResponse>>(messages);
    }
}