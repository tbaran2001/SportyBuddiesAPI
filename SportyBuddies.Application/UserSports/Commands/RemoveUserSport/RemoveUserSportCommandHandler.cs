using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Application.Common.Services;

namespace SportyBuddies.Application.UserSports.Commands.RemoveUserSport;

public class RemoveUserSportCommandHandler(
    IUsersRepository usersRepository,
    ISportsRepository sportsRepository,
    IUnitOfWork unitOfWork,
    IMatchingService matchingService)
    : IRequestHandler<RemoveUserSportCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(RemoveUserSportCommand command, CancellationToken cancellationToken)
    {
        var user = await usersRepository.GetUserByIdWithSportsAsync(command.UserId);
        if (user is null)
            return Error.NotFound(description: "User not found");

        var sport = await sportsRepository.GetSportByIdAsync(command.SportId);
        if (sport is null)
            return Error.NotFound(description: "Sport not found");

        var removeSportResult = user.RemoveSport(sport.Id);

        if (removeSportResult.IsError)
            return removeSportResult.Errors;

        await matchingService.FindMatchesAsync(command.UserId);
        await unitOfWork.CommitChangesAsync();

        return Result.Success;
    }
}