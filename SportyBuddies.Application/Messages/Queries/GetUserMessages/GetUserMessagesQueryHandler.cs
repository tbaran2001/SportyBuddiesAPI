using AutoMapper;
using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.DTOs;
using SportyBuddies.Application.Common.Interfaces;

namespace SportyBuddies.Application.Messages.Queries.GetUserMessages;

public class GetUserMessagesQueryHandler(IMessagesRepository messagesRepository, IMapper mapper)
    : IRequestHandler<GetUserMessagesQuery, ErrorOr<IEnumerable<MessageResponse>>>
{
    public async Task<ErrorOr<IEnumerable<MessageResponse>>> Handle(GetUserMessagesQuery query,
        CancellationToken cancellationToken)
    {
        var messages = await messagesRepository.GetUserMessagesAsync(query.UserId);

        return mapper.Map<List<MessageResponse>>(messages);
    }
}