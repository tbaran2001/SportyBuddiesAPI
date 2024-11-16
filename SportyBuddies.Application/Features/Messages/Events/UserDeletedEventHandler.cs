using MediatR;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Messages;
using SportyBuddies.Domain.Users.Events;

namespace SportyBuddies.Application.Features.Messages.Events;

public class UserDeletedEventHandler(IMessagesRepository messagesRepository, IUnitOfWork unitOfWork)
    : INotificationHandler<UserDeletedEvent>
{
    public async Task Handle(UserDeletedEvent notification, CancellationToken cancellationToken)
    {
        await messagesRepository.RemoveUserMessagesAsync(notification.UserId);
        await unitOfWork.CommitChangesAsync();
    }
}