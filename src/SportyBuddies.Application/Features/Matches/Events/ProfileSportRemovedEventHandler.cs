using MediatR;
using SportyBuddies.Domain.Common.Interfaces.Services;
using SportyBuddies.Domain.Profiles.Events;

namespace SportyBuddies.Application.Features.Matches.Events;

public class ProfileSportRemovedEventHandler(IMatchService matchingService):INotificationHandler<ProfileSportRemovedEvent>
{
    public async Task Handle(ProfileSportRemovedEvent notification, CancellationToken cancellationToken)
    {
        await matchingService.FindMatchesToRemoveAsync(notification.ProfileId);
    }
}