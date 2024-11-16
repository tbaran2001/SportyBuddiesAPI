using MediatR;
using SportyBuddies.Domain.Buddies;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Users.Events;

namespace SportyBuddies.Application.Features.Buddies.Events;

public class UserDeletedEventHandler(IBuddiesRepository buddiesRepository, IUnitOfWork unitOfWork)
    : INotificationHandler<UserDeletedEvent>
{
    public async Task Handle(UserDeletedEvent notification, CancellationToken cancellationToken)
    {
        await buddiesRepository.RemoveUserBuddiesAsync(notification.UserId);
        await unitOfWork.CommitChangesAsync();
    }
}