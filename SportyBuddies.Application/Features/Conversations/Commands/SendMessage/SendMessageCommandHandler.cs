using AutoMapper;
using MediatR;
using SportyBuddies.Application.Common.DTOs.Message;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Conversations;

namespace SportyBuddies.Application.Features.Conversations.Commands.SendMessage;

public class SendMessageCommandHandler(
    IConversationsRepository conversationsRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<SendMessageCommand, MessageResponse>
{
    public async Task<MessageResponse> Handle(SendMessageCommand request, CancellationToken cancellationToken)
    {
        var conversation = await conversationsRepository.GetByIdAsync(request.ConversationId);
        if (conversation == null) throw new NotFoundException(nameof(Conversation), request.ConversationId.ToString());

        var message = Message.Create(request.ConversationId, request.UserId, request.Content);

        await conversationsRepository.AddMessageAsync(message);
        await unitOfWork.CommitChangesAsync();

        return mapper.Map<MessageResponse>(message);
    }
}