using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Application.Common.Services;

namespace SportyBuddies.Application.UserSports.Commands.RemoveUserSport;

public class RemoveUserSportCommandHandler : IRequestHandler<RemoveUserSportCommand, ErrorOr<Success>>
{
    private readonly IMatchingService _matchingService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserSportsRepository _userSportsRepository;


    public RemoveUserSportCommandHandler(IUserSportsRepository userSportsRepository, IUnitOfWork unitOfWork,
        IMatchingService matchingService)
    {
        _userSportsRepository = userSportsRepository;
        _unitOfWork = unitOfWork;
        _matchingService = matchingService;
    }

    public async Task<ErrorOr<Success>> Handle(RemoveUserSportCommand command, CancellationToken cancellationToken)
    {
        await _userSportsRepository.RemoveSportFromUserAsync(command.UserId, command.SportId);

        await _matchingService.FindMatchesAsync(command.UserId);

        await _unitOfWork.CommitChangesAsync();

        return Result.Success;
    }
}