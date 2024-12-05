using MediatR;
using SportyBuddies.Domain.Common.Interfaces.Services;
using SportyBuddies.Domain.Users.Events;

namespace SportyBuddies.Application.Features.Matches.Events;

public class UserSportAddedEventHandler(IMatchService matchingService):INotificationHandler<UserSportAddedEvent>
{
    public async Task Handle(UserSportAddedEvent notification, CancellationToken cancellationToken)
    {
        await matchingService.FindMatchesToAddAsync(notification.UserId);
    }
}