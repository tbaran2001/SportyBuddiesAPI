using MediatR;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Domain.Users.Events;

namespace SportyBuddies.Application.UserSports.Events;

public class UserDeletedEventHandler : INotificationHandler<UserDeletedEvent>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserSportsRepository _userSportsRepository;

    public UserDeletedEventHandler(IUserSportsRepository userSportsRepository, IUnitOfWork unitOfWork)
    {
        _userSportsRepository = userSportsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UserDeletedEvent notification, CancellationToken cancellationToken)
    {
        var userSports = (await _userSportsRepository.GetUserSportsAsync(notification.UserId)).ToList();

        if (!userSports.Any())
            return;

        await _userSportsRepository.RemoveRangeAsync(userSports);
        await _unitOfWork.CommitChangesAsync();
    }
}