using MediatR;
using Microsoft.AspNetCore.SignalR;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Application.Hubs;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Messages;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.Messages.Commands.SendMessage;

public class SendMessageCommandHandler(
    IMessagesRepository messagesRepository,
    IUsersRepository usersRepository,
    IHubContext<ChatHub, IChatClient> hubContext,
    IUnitOfWork unitOfWork) : IRequestHandler<SendMessageCommand>
{
    public async Task Handle(SendMessageCommand command, CancellationToken cancellationToken)
    {
        if (await usersRepository.UserExistsAsync(command.SenderId) == false)
            throw new NotFoundException(nameof(User), command.SenderId.ToString());

        if (await usersRepository.UserExistsAsync(command.RecipientId) == false)
            throw new NotFoundException(nameof(User), command.RecipientId.ToString());

        var message = Message.Create(command.SenderId, command.RecipientId, command.Content);

        await messagesRepository.AddMessageAsync(message);
        await unitOfWork.CommitChangesAsync();

        await hubContext.Clients.User(command.RecipientId.ToString())
            .ReceiveMessage(new HubMessage(message.Id, command.SenderId, command.RecipientId, message.Content,
                message.TimeSent));
        await hubContext.Clients.User(command.SenderId.ToString()).ReceiveMessage(new HubMessage(message.Id,
            command.SenderId,
            command.RecipientId, message.Content, message.TimeSent));
    }
}