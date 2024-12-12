using MediatR;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Profiles.Events;

namespace SportyBuddies.Application.Features.ProfileSports.Events;

public class ProfileDeletedEventHandler(IProfilesRepository iProfilesRepository, IUnitOfWork unitOfWork)
    : INotificationHandler<ProfileDeletedEvent>
{
    public async Task Handle(ProfileDeletedEvent notification, CancellationToken cancellationToken)
    {
        var user= await iProfilesRepository.GetProfileByIdWithSportsAsync(notification.ProfileId);
        if (user is null)
            return;
        
        user.RemoveAllSports();
        await unitOfWork.CommitChangesAsync();
    }
}