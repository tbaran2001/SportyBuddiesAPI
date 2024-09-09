using MediatR;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Application.Common.Services;

namespace SportyBuddies.Application.UserSports.Commands.RemoveUserSport;

public class RemoveUserSportCommandHandler: IRequestHandler<RemoveUserSportCommand>
{
    private readonly IUserSportsRepository _userSportsRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMatchingService _matchingService;


    public RemoveUserSportCommandHandler(IUserSportsRepository userSportsRepository, IUnitOfWork unitOfWork, IMatchingService matchingService)
    {
        _userSportsRepository = userSportsRepository;
        _unitOfWork = unitOfWork;
        _matchingService = matchingService;
    }

    public async Task Handle(RemoveUserSportCommand command, CancellationToken cancellationToken)
    {
        await _userSportsRepository.RemoveSportFromUserAsync(command.UserId, command.SportId);
        
        await _matchingService.FindMatchesAsync(command.UserId);
        
        await _unitOfWork.CommitChangesAsync();
    }
}