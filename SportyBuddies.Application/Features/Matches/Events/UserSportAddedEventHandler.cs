using MediatR;
using SportyBuddies.Application.Common.Services;
using SportyBuddies.Domain.Users.Events;

namespace SportyBuddies.Application.Features.Matches.Events;

public class UserSportAddedEventHandler(IMatchingService matchingService):INotificationHandler<UserSportAddedEvent>
{
    public async Task Handle(UserSportAddedEvent notification, CancellationToken cancellationToken)
    {
        await matchingService.FindMatchesAsync(notification.UserId);
    }
}