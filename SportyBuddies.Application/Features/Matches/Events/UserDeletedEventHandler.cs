using MediatR;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Common.Interfaces;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Matches;
using SportyBuddies.Domain.Users.Events;

namespace SportyBuddies.Application.Features.Matches.Events;

public class UserDeletedEventHandler(IMatchesRepository matchesRepository, IUnitOfWork unitOfWork)
    : INotificationHandler<UserDeletedEvent>
{
    public async Task Handle(UserDeletedEvent notification, CancellationToken cancellationToken)
    {
        var matches = (await matchesRepository.GetUserExistingMatchesAsync(notification.UserId)).ToList();

        if (!matches.Any())
            return;

        await matchesRepository.RemoveRangeAsync(matches);
        await unitOfWork.CommitChangesAsync();
    }
}