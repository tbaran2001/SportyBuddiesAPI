using MediatR;
using SportyBuddies.Application.Common.Services;
using SportyBuddies.Domain.Services;
using SportyBuddies.Domain.Users.Events;

namespace SportyBuddies.Application.Features.Matches.Events;

public class UserSportAddedEventHandler(IMatchService matchingService):INotificationHandler<UserSportAddedEvent>
{
    public async Task Handle(UserSportAddedEvent notification, CancellationToken cancellationToken)
    {
        await matchingService.FindMatchesToAddAsync(notification.UserId);
    }
}