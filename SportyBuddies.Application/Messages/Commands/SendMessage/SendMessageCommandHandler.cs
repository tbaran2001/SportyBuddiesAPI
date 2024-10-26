using MediatR;
using Microsoft.AspNetCore.SignalR;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Application.Hubs;
using SportyBuddies.Domain.Messages;

namespace SportyBuddies.Application.Messages.Commands.SendMessage;

public class SendMessageCommandHandler(
    IMessagesRepository messagesRepository,
    IUsersRepository usersRepository,
    IHubContext<ChatHub, IChatClient> hubContext,
    IUnitOfWork unitOfWork) : IRequestHandler<SendMessageCommand>
{
    public async Task Handle(SendMessageCommand command, CancellationToken cancellationToken)
    {
        var sender = await usersRepository.GetUserByIdAsync(command.SenderId);
        if (sender is null)
            throw new NotFoundException(nameof(sender), command.SenderId.ToString());

        var recipient = await usersRepository.GetUserByIdAsync(command.RecipientId);
        if (recipient is null)
            throw new NotFoundException(nameof(recipient), command.RecipientId.ToString());

        var message = new Message(sender, recipient, command.Content, DateTime.Now);

        await messagesRepository.AddMessageAsync(message);
        await unitOfWork.CommitChangesAsync();

        await hubContext.Clients.User(recipient.Id.ToString())
            .ReceiveMessage(new HubMessage(message.Id, sender.Id, recipient.Id, message.Content, message.TimeSent));
        await hubContext.Clients.User(sender.Id.ToString()).ReceiveMessage(new HubMessage(message.Id, sender.Id,
            recipient.Id, message.Content, message.TimeSent));
    }
}