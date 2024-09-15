using MediatR;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Domain.Users.Events;

namespace SportyBuddies.Application.Matches.Events;

public class UserDeletedEventHandler : INotificationHandler<UserDeletedEvent>
{
    private readonly IMatchesRepository _matchesRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UserDeletedEventHandler(IMatchesRepository matchesRepository, IUnitOfWork unitOfWork)
    {
        _matchesRepository = matchesRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UserDeletedEvent notification, CancellationToken cancellationToken)
    {
        var matches = (await _matchesRepository.GetUserExistingMatchesAsync(notification.UserId)).ToList();

        if (!matches.Any())
            return;

        await _matchesRepository.RemoveRangeAsync(matches);
        await _unitOfWork.CommitChangesAsync();
    }
}