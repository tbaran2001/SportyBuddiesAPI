using AutoMapper;
using MediatR;
using SportyBuddies.Application.Common.DTOs.Message;
using SportyBuddies.Domain.Messages;

namespace SportyBuddies.Application.Messages.Queries.GetUserMessages;

public class GetUserMessagesQueryHandler(IMessagesRepository messagesRepository, IMapper mapper)
    : IRequestHandler<GetUserMessagesQuery, IEnumerable<MessageResponse>>
{
    public async Task<IEnumerable<MessageResponse>> Handle(GetUserMessagesQuery query,
        CancellationToken cancellationToken)
    {
        var messages = await messagesRepository.GetUserMessagesAsync(query.UserId);

        return mapper.Map<List<MessageResponse>>(messages);
    }
}