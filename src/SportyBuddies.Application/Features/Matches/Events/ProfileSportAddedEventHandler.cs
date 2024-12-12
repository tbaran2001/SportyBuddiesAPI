using MediatR;
using SportyBuddies.Domain.Common.Interfaces.Services;
using SportyBuddies.Domain.Profiles.Events;

namespace SportyBuddies.Application.Features.Matches.Events;

public class ProfileSportAddedEventHandler(IMatchService matchingService):INotificationHandler<ProfileSportAddedEvent>
{
    public async Task Handle(ProfileSportAddedEvent notification, CancellationToken cancellationToken)
    {
        await matchingService.FindMatchesToAddAsync(notification.ProfileId);
    }
}