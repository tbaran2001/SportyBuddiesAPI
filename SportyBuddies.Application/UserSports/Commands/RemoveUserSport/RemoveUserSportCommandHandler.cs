using MediatR;
using SportyBuddies.Application.Common.Services;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Sports;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.UserSports.Commands.RemoveUserSport;

public class RemoveUserSportCommandHandler(
    IUsersRepository usersRepository,
    ISportsRepository sportsRepository,
    IUnitOfWork unitOfWork,
    IMatchingService matchingService)
    : IRequestHandler<RemoveUserSportCommand>
{
    public async Task Handle(RemoveUserSportCommand command, CancellationToken cancellationToken)
    {
        var user = await usersRepository.GetUserByIdWithSportsAsync(command.UserId);
        if (user is null)
            throw new NotFoundException(nameof(user), command.UserId.ToString());

        var sport = await sportsRepository.GetSportByIdAsync(command.SportId);
        if (sport is null)
            throw new NotFoundException(nameof(sport), command.SportId.ToString());

        user.RemoveSport(sport);

        await matchingService.FindMatchesAsync(command.UserId);
        await unitOfWork.CommitChangesAsync();
    }
}