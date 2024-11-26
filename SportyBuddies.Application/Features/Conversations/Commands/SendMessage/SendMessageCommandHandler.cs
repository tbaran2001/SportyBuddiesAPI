using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using SportyBuddies.Application.Common.DTOs.Conversation;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Application.Hubs;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Common.Interfaces;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Conversations;

namespace SportyBuddies.Application.Features.Conversations.Commands.SendMessage;

public class SendMessageCommandHandler(
    IConversationsRepository conversationsRepository,
    IHubContext<ChatHub, IChatClient> hubContext,
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<SendMessageCommand, MessageResponse>
{
    public async Task<MessageResponse> Handle(SendMessageCommand command, CancellationToken cancellationToken)
    {
        var conversation = await conversationsRepository.GetByIdAsync(command.ConversationId);
        if (conversation is null)
            throw new NotFoundException(nameof(Conversation), command.ConversationId.ToString());

        var message = Message.Create(command.ConversationId, command.UserId, command.Content);

        await conversationsRepository.AddMessageAsync(message);
        await unitOfWork.CommitChangesAsync();

        await hubContext.Clients.Users(conversation.Participants.Select(p => p.UserId.ToString()))
            .ReceiveMessage(new HubMessage(conversation.Id, message.SenderId, message.Content, message.CreatedOnUtc,
                conversation.Participants.Select(p => p.UserId).ToList()));

        return mapper.Map<MessageResponse>(message);
    }
}