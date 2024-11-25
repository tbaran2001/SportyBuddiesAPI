using MediatR;
using SportyBuddies.Application.Common.Services;
using SportyBuddies.Domain.Services;
using SportyBuddies.Domain.Users.Events;

namespace SportyBuddies.Application.Features.Matches.Events;

public class UserSportRemovedEventHandler(IMatchService matchingService):INotificationHandler<UserSportRemovedEvent>
{
    public async Task Handle(UserSportRemovedEvent notification, CancellationToken cancellationToken)
    {
        await matchingService.FindMatchesToRemoveAsync(notification.UserId);
    }
}