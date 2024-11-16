using MediatR;
using SportyBuddies.Application.Common.Services;
using SportyBuddies.Domain.Users.Events;

namespace SportyBuddies.Application.Features.Matches.Events;

public class UserSportRemovedEventHandler(IMatchingService matchingService):INotificationHandler<UserSportRemovedEvent>
{
    public async Task Handle(UserSportRemovedEvent notification, CancellationToken cancellationToken)
    {
        await matchingService.FindMatchesAsync(notification.UserId);
    }
}