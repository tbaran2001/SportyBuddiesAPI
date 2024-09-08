using MediatR;
using SportyBuddies.Application.Common.Interfaces;

namespace SportyBuddies.Application.UserSports.Commands.RemoveUserSport;

public class RemoveUserSportCommandHandler: IRequestHandler<RemoveUserSportCommand>
{
    private readonly IUserSportsRepository _userSportsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveUserSportCommandHandler(IUserSportsRepository userSportsRepository, IUnitOfWork unitOfWork)
    {
        _userSportsRepository = userSportsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(RemoveUserSportCommand request, CancellationToken cancellationToken)
    {
        await _userSportsRepository.RemoveSportFromUserAsync(request.UserId, request.SportId);
        await _unitOfWork.CommitChangesAsync();
    }
}