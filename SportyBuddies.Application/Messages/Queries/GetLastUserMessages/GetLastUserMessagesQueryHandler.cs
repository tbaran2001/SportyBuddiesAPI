using AutoMapper;
using MediatR;
using SportyBuddies.Application.Common.DTOs.Message;
using SportyBuddies.Domain.Messages;

namespace SportyBuddies.Application.Messages.Queries.GetLastUserMessages;

public class GetLastUserMessagesQueryHandler(IMessagesRepository messagesRepository, IMapper mapper)
    : IRequestHandler<GetLastUserMessagesQuery, List<MessageResponse>>
{
    public async Task<List<MessageResponse>> Handle(GetLastUserMessagesQuery request,
        CancellationToken cancellationToken)
    {
        var messages = await messagesRepository.GetLastUserMessagesAsync(request.UserId);

        return mapper.Map<List<MessageResponse>>(messages);
    }
}