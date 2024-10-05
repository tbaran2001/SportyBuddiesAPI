using MediatR;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Domain.Users.Events;

namespace SportyBuddies.Application.UserSports.Events;

public class UserDeletedEventHandler(IUserSportsRepository userSportsRepository, IUnitOfWork unitOfWork)
    : INotificationHandler<UserDeletedEvent>
{
    public async Task Handle(UserDeletedEvent notification, CancellationToken cancellationToken)
    {
        var userSports = (await userSportsRepository.GetUserSportsAsync(notification.UserId)).ToList();

        if (!userSports.Any())
            return;

        await userSportsRepository.RemoveRangeAsync(userSports);
        await unitOfWork.CommitChangesAsync();
    }
}