using AutoMapper;
using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.DTOs.Message;
using SportyBuddies.Application.Common.Interfaces;

namespace SportyBuddies.Application.Messages.Queries.GetLastUserMessages;

public class GetLastUserMessagesQueryHandler(IMessagesRepository messagesRepository, IMapper mapper)
    : IRequestHandler<GetLastUserMessagesQuery, ErrorOr<IEnumerable<MessageResponse>>>
{
    public async Task<ErrorOr<IEnumerable<MessageResponse>>> Handle(GetLastUserMessagesQuery request,
        CancellationToken cancellationToken)
    {
        var messages = await messagesRepository.GetLastUserMessagesAsync(request.UserId);

        return mapper.Map<List<MessageResponse>>(messages);
    }
}