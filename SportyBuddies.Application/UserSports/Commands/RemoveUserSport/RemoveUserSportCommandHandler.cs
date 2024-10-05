using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Application.Common.Services;

namespace SportyBuddies.Application.UserSports.Commands.RemoveUserSport;

public class RemoveUserSportCommandHandler(
    IUserSportsRepository userSportsRepository,
    IUnitOfWork unitOfWork,
    IMatchingService matchingService)
    : IRequestHandler<RemoveUserSportCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(RemoveUserSportCommand command, CancellationToken cancellationToken)
    {
        await userSportsRepository.RemoveSportFromUserAsync(command.UserId, command.SportId);

        await matchingService.FindMatchesAsync(command.UserId);

        await unitOfWork.CommitChangesAsync();

        return Result.Success;
    }
}