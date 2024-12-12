using MediatR;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Profiles.Events;

namespace SportyBuddies.Application.Features.Matches.Events;

public class ProfileDeletedEventHandler(IMatchesRepository matchesRepository, IUnitOfWork unitOfWork)
    : INotificationHandler<ProfileDeletedEvent>
{
    public async Task Handle(ProfileDeletedEvent notification, CancellationToken cancellationToken)
    {
        var matches = (await matchesRepository.GetProfileExistingMatchesAsync(notification.ProfileId)).ToList();

        if (!matches.Any())
            return;

        await matchesRepository.RemoveRangeAsync(matches);
        await unitOfWork.CommitChangesAsync();
    }
}