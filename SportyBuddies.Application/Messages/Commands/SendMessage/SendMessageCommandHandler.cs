using AutoMapper;
using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Domain.Messages;

namespace SportyBuddies.Application.Messages.Commands.SendMessage;

public class SendMessageCommandHandler(
    IMessagesRepository messagesRepository,
    IUsersRepository usersRepository,
    IMapper mapper,
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

        return Result.Success;
    }
}