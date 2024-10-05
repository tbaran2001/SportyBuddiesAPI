using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Application.Common.Services;

namespace SportyBuddies.Application.UserSports.Commands.AddUserSport;

public class AddUserSportCommandHandler(
    IUserSportsRepository userSportsRepository,
    IUnitOfWork unitOfWork,
    IMatchingService matchingService)
    : IRequestHandler<AddUserSportCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(AddUserSportCommand command, CancellationToken cancellationToken)
    {
        await userSportsRepository.AddSportToUserAsync(command.UserId, command.SportId);

        await matchingService.FindMatchesAsync(command.UserId);

        await unitOfWork.CommitChangesAsync();

        return Result.Success;
    }
}