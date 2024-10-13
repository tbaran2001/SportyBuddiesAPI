using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Application.Common.Services;

namespace SportyBuddies.Application.UserSports.Commands.AddUserSport;

public class AddUserSportCommandHandler(
    IUsersRepository usersRepository,
    ISportsRepository sportsRepository,
    IUnitOfWork unitOfWork,
    IMatchingService matchingService)
    : IRequestHandler<AddUserSportCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(AddUserSportCommand command, CancellationToken cancellationToken)
    {
        var user = await usersRepository.GetUserByIdWithSportsAsync(command.UserId);
        if (user is null)
            return Error.NotFound(description: "User not found");

        var sport = await sportsRepository.GetSportByIdAsync(command.SportId);
        if (sport is null)
            return Error.NotFound(description: "Sport not found");

        var addSportResult = user.AddSport(sport.Id);

        if (addSportResult.IsError)
            return addSportResult.Errors;

        await matchingService.FindMatchesAsync(command.UserId);
        await unitOfWork.CommitChangesAsync();

        return Result.Success;
    }
}