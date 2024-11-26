using MediatR;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Common.Interfaces;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Sports;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.Features.UserSports.Commands.AddUserSport;

public class AddUserSportCommandHandler(
    IUsersRepository usersRepository,
    ISportsRepository sportsRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<AddUserSportCommand>
{
    public async Task Handle(AddUserSportCommand command, CancellationToken cancellationToken)
    {
        var user = await usersRepository.GetUserByIdWithSportsAsync(command.UserId);
        if (user is null)
            throw new NotFoundException(nameof(user), command.UserId.ToString());

        var sport = await sportsRepository.GetSportByIdAsync(command.SportId);
        if (sport is null)
            throw new NotFoundException(nameof(sport), command.SportId.ToString());

        user.AddSport(sport);

        await unitOfWork.CommitChangesAsync();
    }
}