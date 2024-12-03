using MediatR;
using SportyBuddies.Application.Authentication;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Common.Interfaces.Repositories;

namespace SportyBuddies.Application.Features.UserSports.Commands.RemoveUserSport;

public class RemoveUserSportCommandHandler(
    IUsersRepository usersRepository,
    ISportsRepository sportsRepository,
    IUnitOfWork unitOfWork,
    IUserContext userContext)
    : IRequestHandler<RemoveUserSportCommand>
{
    public async Task Handle(RemoveUserSportCommand command, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();

        var user = await usersRepository.GetUserByIdWithSportsAsync(currentUser!.Id);
        if (user is null)
            throw new NotFoundException(nameof(user), currentUser.Id.ToString());

        var sport = await sportsRepository.GetSportByIdAsync(command.SportId);
        if (sport is null)
            throw new NotFoundException(nameof(sport), command.SportId.ToString());

        user.RemoveSport(sport);

        await unitOfWork.CommitChangesAsync();
    }
}