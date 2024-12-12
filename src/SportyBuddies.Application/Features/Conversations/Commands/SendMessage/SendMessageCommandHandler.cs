using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using SportyBuddies.Application.Authentication;
using SportyBuddies.Application.Common.DTOs.Conversation;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Application.Hubs;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Conversations;

namespace SportyBuddies.Application.Features.Conversations.Commands.SendMessage;

public class SendMessageCommandHandler(
    IConversationsRepository conversationsRepository,
    IHubContext<ChatHub, IChatClient> hubContext,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IUserContext userContext)
    : IRequestHandler<SendMessageCommand, MessageResponse>
{
    public async Task<MessageResponse> Handle(SendMessageCommand command, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();

        var conversation = await conversationsRepository.GetByIdAsync(command.ConversationId);
        if (conversation is null)
            throw new NotFoundException(nameof(Conversation), command.ConversationId.ToString());

        var message = Message.Create(command.ConversationId, currentUser.Id, command.Content);

        await conversationsRepository.AddMessageAsync(message);
        await unitOfWork.CommitChangesAsync();

        await hubContext.Clients.Users(conversation.Participants.Select(p => p.ProfileId.ToString()))
            .ReceiveMessage(new HubMessage(conversation.Id, message.SenderId, message.Content, message.CreatedOnUtc,
                conversation.Participants.Select(p => p.ProfileId).ToList()));

        return mapper.Map<MessageResponse>(message);
    }
}