using MediatR;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Application.Common.Services;

namespace SportyBuddies.Application.UserSports.Commands.AddUserSport;

public class AddUserSportCommandHandler: IRequestHandler<AddUserSportCommand>
{
    private readonly IUserSportsRepository _userSportsRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMatchingService _matchingService;

    public AddUserSportCommandHandler(IUserSportsRepository userSportsRepository, IUnitOfWork unitOfWork, IMatchingService matchingService)
    {
        _userSportsRepository = userSportsRepository;
        _unitOfWork = unitOfWork;
        _matchingService = matchingService;
    }

    public async Task Handle(AddUserSportCommand command, CancellationToken cancellationToken)
    {
        await _userSportsRepository.AddSportToUserAsync(command.UserId, command.SportId);

        await _matchingService.FindMatchesAsync(command.UserId);
        
        await _unitOfWork.CommitChangesAsync();
    }
}