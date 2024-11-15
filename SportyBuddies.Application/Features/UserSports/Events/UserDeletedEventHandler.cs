using MediatR;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Users;
using SportyBuddies.Domain.Users.Events;

namespace SportyBuddies.Application.Features.UserSports.Events;

public class UserDeletedEventHandler(IUsersRepository usersRepository, IUnitOfWork unitOfWork)
    : INotificationHandler<UserDeletedEvent>
{
    public async Task Handle(UserDeletedEvent notification, CancellationToken cancellationToken)
    {
        var user= await usersRepository.GetUserByIdWithSportsAsync(notification.UserId);
        if (user is null)
            return;
        
        user.RemoveAllSports();
        await unitOfWork.CommitChangesAsync();
    }
}