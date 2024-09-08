using MediatR;
using SportyBuddies.Application.Common.Interfaces;

namespace SportyBuddies.Application.UserSports.Commands.AddUserSport;

public class AddUserSportCommandHandler: IRequestHandler<AddUserSportCommand>
{
    private readonly IUserSportsRepository _userSportsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddUserSportCommandHandler(IUserSportsRepository userSportsRepository, IUnitOfWork unitOfWork)
    {
        _userSportsRepository = userSportsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(AddUserSportCommand command, CancellationToken cancellationToken)
    {
        await _userSportsRepository.AddSportToUserAsync(command.UserId, command.SportId);
        await _unitOfWork.CommitChangesAsync();
    }
}