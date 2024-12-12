using MediatR;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Profiles.Events;

namespace SportyBuddies.Application.Features.Buddies.Events;

public class ProfileDeletedEventHandler(IBuddiesRepository buddiesRepository, IUnitOfWork unitOfWork)
    : INotificationHandler<ProfileDeletedEvent>
{
    public async Task Handle(ProfileDeletedEvent notification, CancellationToken cancellationToken)
    {
        await buddiesRepository.RemoveProfileBuddiesAsync(notification.ProfileId);
        await unitOfWork.CommitChangesAsync();
    }
}