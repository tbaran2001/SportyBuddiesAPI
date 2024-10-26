using MediatR;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Matches;
using SportyBuddies.Domain.Users.Events;

namespace SportyBuddies.Application.Matches.Events;

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