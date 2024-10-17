using AutoMapper;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Application.Hubs;
using SportyBuddies.Domain.Messages;

namespace SportyBuddies.Application.Messages.Commands.SendMessage;

public class SendMessageCommandHandler(
    IMessagesRepository messagesRepository,
    IUsersRepository usersRepository,
    IMapper mapper,
    IHubContext<ChatHub, IChatClient> hubContext,
    IUnitOfWork unitOfWork) : IRequestHandler<SendMessageCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(SendMessageCommand command, CancellationToken cancellationToken)
    {
        var sender = await usersRepository.GetUserByIdAsync(command.SenderId);
        if (sender is null)
            return Error.NotFound(description: "Sender not found");

        var recipient = await usersRepository.GetUserByIdAsync(command.RecipientId);
        if (recipient is null)
            return Error.NotFound(description: "Recipient not found");

        var message = new Message(sender, recipient, command.Content, DateTime.Now);

        await messagesRepository.AddMessageAsync(message);
        await unitOfWork.CommitChangesAsync();

        await hubContext.Clients.User(recipient.Id.ToString())
            .ReceiveMessage(new HubMessage(message.Id, sender.Id, recipient.Id, message.Content, message.TimeSent));
        await hubContext.Clients.User(sender.Id.ToString()).ReceiveMessage(new HubMessage(message.Id, sender.Id,
            recipient.Id, message.Content, message.TimeSent));

        return Result.Success;
    }
}